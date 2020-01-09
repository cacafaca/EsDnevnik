﻿using ProCode.EsDnevnik.Model;
using System.Collections.ObjectModel;
using Prism.Navigation;
using System.Linq;
using System.Threading.Tasks;
using ProCode.EsDnevnik.Model.GeneratedGrades;
using Prism.Commands;
using Xamarin.Forms;
using System;
using Prism.Services;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class StudentOverviewPageViewModel : ViewModelBase, INavigatedAware
    {
        public StudentOverviewPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Преглед за дете";
            timeLineEvents = new ObservableCollection<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent>();
            coursesGrades = new ObservableCollection<CourseGrades>();
            this.dialogService = dialogService;
        }

        EsDnevnik.Service.EsDnevnik esdService;
        Student Student;
        private readonly IPageDialogService dialogService;


        #region Time Line Events tab
        private ObservableCollection<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent> timeLineEvents;
        public ObservableCollection<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent> TimeLineEvents
        {
            get { return timeLineEvents; }
            set { SetProperty(ref timeLineEvents, value); }
        }
        private bool timeLineEventsPopulated = false;

        private async Task LoadTimeLine(TimeLineLoadType loadType)
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
                        foreach (var timeLineDate in newRootTimeLine.Data.OrderByDescending(date => date.Key))
                            foreach (var timeLineEvent in timeLineDate.Value.OrderByDescending(ev => ev.SchoolHour))
                                TimeLineEvents.Add(timeLineEvent);
                        timeLineEventsPopulated = newRootTimeLine.Data.Count == 0; // If no elements are returned it means that complete list is retrieved.
                    }
                }
                catch (Exception ex)
                {
                    await dialogService.DisplayAlertAsync("Грешка?", ex.Message, "Уреду");
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
                _ = LoadTimeLine(TimeLineLoadType.NextPage);
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

        public async Task LoadGrades(Student student)
        {
            if (CoursesGrades.Count == 0)
            {
                EsDnevnik.Model.GeneratedGrades.Rootobject gradesRoot = null;
#if !DEBUGFAKE
                gradesRoot = await esdService.GetGradesAsync(Student);
#else
                await Task.Run(() => { gradesRoot = esdService.GetGradesFake(); });
#endif
                CoursesGrades.Clear();
                foreach (var courseGrade in gradesRoot.Courses.OrderBy(ev => ev, new GradesComparer<CourseGrades>()))
                    CoursesGrades.Add(courseGrade);
            }
        }

        #endregion


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            // Validate params.
            if (esdService == null)
                esdService = parameters.GetValue<EsDnevnik.Service.EsDnevnik>(StudentListPageViewModel.GetEsdServiceParamName());
            if (Student == null)
            {
                Student = parameters.GetValue<Student>(nameof(StudentListPageViewModel.SelectedStudent));
                Title = Student.FullName;
            }

            // Time line data fetch 
            await LoadTimeLine(TimeLineLoadType.Refresh);

            // Grades fetch
            await LoadGrades(Student);
        }
    }

    enum TimeLineLoadType
    {
        Refresh,
        NextPage
    }
}