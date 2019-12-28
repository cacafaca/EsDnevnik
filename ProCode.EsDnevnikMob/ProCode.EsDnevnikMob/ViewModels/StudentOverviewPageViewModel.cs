using ProCode.EsDnevnik.Model;
using System.Collections.ObjectModel;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProCode.EsDnevnik.Model.GeneratedGrades;
using System;
#if DEBUGFAKE
using System.Threading.Tasks;
#endif

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class StudentOverviewPageViewModel : ViewModelBase, INavigatedAware
    {
        public StudentOverviewPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pregled za dete";
            timeLineEvents = new ObservableCollection<TimeLineEvent>();
            grades = new ObservableCollection<EsDnevnik.Model.GeneratedGrades.GradesArray>();
        }

        EsDnevnik.Service.EsDnevnik esdService;
        Student Student;


        #region Time Line Events tab
        private ObservableCollection<TimeLineEvent> timeLineEvents;
        public ObservableCollection<TimeLineEvent> TimeLineEvents
        {
            get { return timeLineEvents; }
            set { SetProperty(ref timeLineEvents, value); }
        }

        private async Task LoadTimeLine(Student student)
        {
            if (TimeLineEvents.Count == 0)
            {
                IList<TimeLineEvent> newTimeLineEvents;
#if !DEBUGFAKE
                newTimeLineEvents = await esdService.GetTimeLineEventsAsync(Student);
#else
                await Task.Run(() => { timeLine = esdService.GetTimeLineFake(); });
#endif
                TimeLineEvents.Clear();
                foreach (var timeLineEvent in newTimeLineEvents.OrderByDescending(ev => ev.CreateTime))
                    TimeLineEvents.Add(timeLineEvent);
            }
        }
        #endregion


        #region Grades Tab
        private ObservableCollection<ProCode.EsDnevnik.Model.GeneratedGrades.GradesArray> grades;

        public ObservableCollection<ProCode.EsDnevnik.Model.GeneratedGrades.GradesArray> Grades
        {
            get { return grades; }
            set { SetProperty(ref grades, value); }
        }

        public async Task LoadGrades(Student student)
        {
            if (Grades.Count == 0)
            {
                EsDnevnik.Model.GeneratedGrades.Rootobject gradesRoot;
#if !DEBUGFAKE
                gradesRoot = await esdService.GetGradesAsync(Student);
#else
                gradesRoot = esdService.GetGradesFake();
#endif
                Grades.Clear();
                foreach (var grade in gradesRoot.Grades.OrderByDescending(ev => ev.Course))
                    Grades.Add(grade);
                GeneratedGradesSimple = CalculateGeneratedGradesSimple(Grades);
            }
        }

        private ObservableCollection<GeneratedGradesSimple> CalculateGeneratedGradesSimple(ObservableCollection<GradesArray> grades)
        {
            ObservableCollection<GeneratedGradesSimple> gradesSimple = new ObservableCollection<GeneratedGradesSimple>();
            foreach(var grade in grades)
            {
                foreach(var courseGrade in grade.Parts.Part1Value.Grades)
                {
                    gradesSimple.Add(new EsDnevnik.Model.GeneratedGradesSimple
                    {
                        Course = grade.Course,
                        Date = courseGrade.Date,
                        Grade = courseGrade.GradeValue,
                        FullGrade = courseGrade.FullGrade,
                        GradeCategory = courseGrade.GradeCategory,
                        Note = courseGrade.Note,
                        Average = grade.Parts.Part1Value.Average
                    });
                }
            }
            ObservableCollection<GeneratedGradesSimple> gradesSimpleSorted = new ObservableCollection<GeneratedGradesSimple>();
            foreach(var grade in gradesSimple.OrderByDescending(gr => gr.Date))
            {
                gradesSimpleSorted.Add(grade);
            }
            return gradesSimpleSorted;
        }

        private ObservableCollection<EsDnevnik.Model.GeneratedGradesSimple> generatedGradesSimples;

        public ObservableCollection<EsDnevnik.Model.GeneratedGradesSimple> GeneratedGradesSimple
        {
            get { return generatedGradesSimples; }
            set
            {
                SetProperty(ref generatedGradesSimples, value);
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
            await LoadTimeLine(Student);

            // Grades fetch
            await LoadGrades(Student);
        }
    }
}