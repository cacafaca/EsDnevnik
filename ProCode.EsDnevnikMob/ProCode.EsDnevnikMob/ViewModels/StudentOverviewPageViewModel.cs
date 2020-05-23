using ProCode.EsDnevnik.Model;
using System.Collections.ObjectModel;
using Prism.Navigation;
using System.Linq;
using System.Threading.Tasks;
using ProCode.EsDnevnik.Model.GeneratedGrades;
using Prism.Commands;
using Xamarin.Forms;
using System;
using Prism.Services;
using System.Collections.Generic;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class StudentOverviewPageViewModel : ViewModelBase, INavigatedAware
    {
        public StudentOverviewPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Преглед за дете";
            timeLineEvents = new ObservableCollection<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent>();
            coursesGrades = new ObservableCollection<CourseGrades>();
            absences = new ObservableCollection<EsDnevnik.Model.GeneratedAbsences.AbsenceSequence>();
        }

        EsDnevnik.Service.EsDnevnik esdService;
        Student Student;

        #region Time Line Events tab
        private ObservableCollection<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent> timeLineEvents;
        public ObservableCollection<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent> TimeLineEvents
        {
            get { return timeLineEvents; }
            set { SetProperty(ref timeLineEvents, value); }
        }
        private bool timeLineEventsPopulated = false;

        private async Task LoadTimeLineAsync(TimeLineLoadType loadType)
        {
            EsDnevnik.Model.GeneratedTimeLine.Rootobject newRootTimeLine = null;
            bool resetTimleLineEventPage = loadType == TimeLineLoadType.Refresh;
            if (resetTimleLineEventPage)
            {
                TimeLineEvents.Clear();
                timeLineEventsPopulated = false;
            }

            if (!timeLineEventsPopulated)
                try
                {
                    AnimateLoading(true);
#if !DEBUGFAKE
                    newRootTimeLine = await esdService.GetTimeLineEventsAsync(Student, resetTimleLineEventPage);
#else
                    await Task.Run(() => { newRootTimeLine = esdService.GetTimeLineEventsFake(); });
#endif
                    if (newRootTimeLine != null && newRootTimeLine.Data != null)
                    {
                        IList<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent> newTimeLineEvents = new List<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent>();
                        foreach (var timeLineDate in newRootTimeLine.Data)
                            foreach (var timeLineEvent in timeLineDate.Value)
                                newTimeLineEvents.Add(timeLineEvent);

                        var allTimeLineEvents = TimeLineEvents.Union(newTimeLineEvents).ToList();
                        TimeLineEvents.Clear();
                        foreach (var timeLineEvent in allTimeLineEvents.OrderByDescending(e => e.Date).ThenByDescending(e => e.SchoolHour))
                            TimeLineEvents.Add(timeLineEvent);


                        timeLineEventsPopulated = newRootTimeLine.Data.Count == 0; // If no elements are returned it means that complete list is retrieved.
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync(ex);
                }
                finally
                {
                    AnimateLoading(false);
                }
        }

        private void AnimateLoading(bool loading)
        {
            if (loading)
            {
                if (!TimeLineEvents.Any(t => t.Type == EsDnevnik.Model.GeneratedTimeLine.EventType.Loading))
                    TimeLineEvents.Add(new EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent { Type = EsDnevnik.Model.GeneratedTimeLine.EventType.Loading });
            }
            else
            {
                TimeLineEvents.Remove(TimeLineEvents.Where(tle => tle.Type == EsDnevnik.Model.GeneratedTimeLine.EventType.Loading).FirstOrDefault());
            }
        }

        private DelegateCommand<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent> timeLineItemAppearingCommand;
        public DelegateCommand<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent> TimeLineItemAppearingCommand => timeLineItemAppearingCommand ?? (timeLineItemAppearingCommand = new DelegateCommand<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent>(ExecuteTimeLineItemAppearingCommand));

        private void ExecuteTimeLineItemAppearingCommand(EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent timeLineEvent)
        {
            if (!TimeLineEvents.Any(tle => tle.Type == EsDnevnik.Model.GeneratedTimeLine.EventType.Loading) && timeLineEvent != null && timeLineEvent == TimeLineEvents.LastOrDefault())
            {
                _ = LoadTimeLineAsync(TimeLineLoadType.NextPage);
            }
        }
        #endregion


        #region Grades Tab
        private ObservableCollection<CourseGrades> coursesGrades;

        public ObservableCollection<CourseGrades> CoursesGrades
        {
            get { return coursesGrades; }
            set { SetProperty(ref coursesGrades, value); }
        }

        public async Task LoadGradesAsync(Student student)
        {
            if (CoursesGrades.Count == 0)
            {
                Rootobject gradesRoot = null;
                try
                {
#if !DEBUGFAKE
                    gradesRoot = await esdService.GetGradesAsync(Student);
#else
                    await Task.Run(() => { gradesRoot = esdService.GetGradesFake(); });
#endif
                    CoursesGrades.Clear();
                    foreach (var courseGrade in gradesRoot.Courses.OrderBy(ev => ev, new GradesComparer<CourseGrades>()))
                        CoursesGrades.Add(courseGrade);
                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync(ex);
                }
            }
        }

        #endregion


        #region Absences tab
        private ObservableCollection<EsDnevnik.Model.GeneratedAbsences.AbsenceSequence> absences;

        public ObservableCollection<EsDnevnik.Model.GeneratedAbsences.AbsenceSequence> Absences
        {
            get { return absences; }
            set { SetProperty(ref absences, value); }
        }

        private async Task LoadAbsencesAsync(Student student)
        {
            EsDnevnik.Model.GeneratedAbsences.AbsencesRoot absencesRoot = null;
            try
            {
#if !DEBUGFAKE
                absencesRoot = await esdService.GetAbsencesAsync(Student);
#else
                await Task.Run(() => { absencesRoot = esdService.GetAbsencesFake(); });
#endif
                Absences.Clear();
                foreach (var absence in absencesRoot.Values.OrderByDescending(a => a.AbsentStatuses.Unregulated?.Absents.Length).
                    ThenByDescending(a => a.AbsentStatuses.Unjustified?.Absents.Length).
                    ThenByDescending(a => a.AbsentStatuses.Justified?.Absents.Length))
                    Absences.Add(absence);
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(ex);
            }
        }
        #endregion

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            // Validate parameters.
            if (esdService == null)
                esdService = parameters.GetValue<EsDnevnik.Service.EsDnevnik>(StudentListPageViewModel.GetEsdServiceParamName());
            if (Student == null)
            {
                Student = parameters.GetValue<Student>(nameof(StudentListPageViewModel.SelectedStudent));
                Title = Student.FullName;
            }

            // Time line data fetch 
            await LoadTimeLineAsync(TimeLineLoadType.Refresh);

            // Grades data fetch.
            await LoadGradesAsync(Student);

            // Absences data fetch.
            await LoadAbsencesAsync(Student);
        }
    }

    enum TimeLineLoadType
    {
        Refresh,
        NextPage
    }
}