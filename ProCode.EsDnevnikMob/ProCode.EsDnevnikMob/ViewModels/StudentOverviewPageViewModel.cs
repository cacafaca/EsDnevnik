using ProCode.EsDnevnik.Model;
using System.Collections.ObjectModel;
using Prism.Navigation;
using System.Collections.Generic;
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
        private ObservableCollection<TimeLineEvent> timeLineEvents;
        public ObservableCollection<TimeLineEvent> TimeLineEvents
        {
            get { return timeLineEvents; }
            set { SetProperty(ref timeLineEvents, value); }
        }

        EsDnevnik.Service.EsDnevnik esdService;
        Student Student;

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (esdService == null)
                esdService = parameters.GetValue<EsDnevnik.Service.EsDnevnik>(StudentListPageViewModel.GetEsdServiceParamName());
            if (Student == null)
            {
                Student = parameters.GetValue<Student>(nameof(StudentListPageViewModel.SelectedStudent));
                Title = Student.FullName;
            }

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
                    TimeLineEvents.Add(eventInTimeLine);
                }
            }
        }
    }
}