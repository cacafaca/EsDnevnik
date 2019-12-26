using ProCode.EsDnevnik.Model;
using System.Collections.ObjectModel;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
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
        }

        #region Time Line Events tab
        private ObservableCollection<TimeLineEvent> timeLineEvents;
        public ObservableCollection<TimeLineEvent> TimeLineEvents
        {
            get { return timeLineEvents; }
            set { SetProperty(ref timeLineEvents, value); }
        }

        EsDnevnik.Service.EsDnevnik esdService;
        Student Student;

        #endregion

        #region Grades Tab
        private ObservableCollection<Grade> grades;

        public ObservableCollection<Grade> Grades
        {
            get { return grades; }
            set { SetProperty(ref grades, value); }
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
            var newTimeLineEvents = new List<TimeLineEvent>();
            if (TimeLineEvents.Count == 0)
            {
                IList<TimeLineEvent> timeLine = null;
#if !DEBUGFAKE
                timeLine = await esdService.GetTimeLineEventsAsync(Student);
#else
                await Task.Run(() => { timeLine = esdService.GetTimeLineFake(); });
#endif
                foreach (var eventInTimeLine in timeLine)
                {
                    newTimeLineEvents.Add(eventInTimeLine);
                }
                TimeLineEvents.Clear();
                foreach (var timeLineEvent in newTimeLineEvents.OrderByDescending(ev => ev.CreateTime))
                    TimeLineEvents.Add(timeLineEvent);
            }

            // Grades fetch
            
        }
    }
}