using ProCode.EsDnevnik.Model;
using System.Collections.ObjectModel;
using Prism.Navigation;
using System.Linq;
using System.Threading.Tasks;
using ProCode.EsDnevnik.Model.GeneratedGrades;
using Prism.Commands;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class StudentOverviewPageViewModel : ViewModelBase, INavigatedAware
    {
        public StudentOverviewPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Преглед за дете";
            timeLineEvents = new ObservableCollection<EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent>();
            grades = new ObservableCollection<GradesArray>();
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

        private async Task LoadTimeLine(Student student, TimeLineLoadType loadType)
        {
            if (TimeLineEvents.Count == 0)
            {
                EsDnevnik.Model.GeneratedTimeLine.Rootobject newRootTimeLine = null;
                bool resetTimeLineEventPage = loadType == TimeLineLoadType.Refresh;
#if !DEBUGFAKE
                newRootTimeLine = await esdService.GetTimeLineEventsAsync(Student);
#else
                await Task.Run(() => { newRootTimeLine = esdService.GetTimeLineEventsFake(); });
#endif
                TimeLineEvents.Clear();
                foreach (var timeLineDate in newRootTimeLine.Data.OrderByDescending(ev => ev.Key))
                    foreach (var timeLineEvent in timeLineDate.Value)
                        TimeLineEvents.Add(timeLineEvent);
            }
        }

        private DelegateCommand timeLineScrolledCommand;
        public DelegateCommand TimeLineScrolledCommand => timeLineScrolledCommand ?? (timeLineScrolledCommand = new DelegateCommand(ExecuteTimeLineScrolledCommand));

        private void ExecuteTimeLineScrolledCommand()
        {

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
                EsDnevnik.Model.GeneratedGrades.Rootobject gradesRoot = null;
#if !DEBUGFAKE
                gradesRoot = await esdService.GetGradesAsync(Student);
#else
                await Task.Run(() => { gradesRoot = esdService.GetGradesFake(); });
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
            foreach (var grade in grades)
            {
                foreach (var courseGrade in grade.Parts.Part1Value.Grades)
                {
                    gradesSimple.Add(new GeneratedGradesSimple
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
            foreach (var grade in gradesSimple.OrderByDescending(gr => gr.Date))
            {
                gradesSimpleSorted.Add(grade);
            }
            return gradesSimpleSorted;
        }

        private ObservableCollection<GeneratedGradesSimple> generatedGradesSimples;

        public ObservableCollection<GeneratedGradesSimple> GeneratedGradesSimple
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
            await LoadTimeLine(Student, TimeLineLoadType.Refresh);

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