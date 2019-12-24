using ProCode.EsDnevnik.Model;
using System.Collections.ObjectModel;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class StudentOverviewPageViewModel : ViewModelBase, INavigatedAware
    {
        public StudentOverviewPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pregled za dete";
            timeLine = new ObservableCollection<TimeLine>();
        }
        private ObservableCollection<TimeLine> timeLine;
        public ObservableCollection<TimeLine> TimeLine
        {
            get { return timeLine; }
            set { SetProperty(ref timeLine, value); }
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

            if (TimeLine.Count == 0)
            {
                IList<TimeLine> timeLine = null;
#if !DEBUGFAKE
            timeLine = await esdService.GetTimeLineAsync();
#else
                await Task.Run(() => { timeLine = esdService.GetTimeLineFake(); });
#endif
                foreach (var eventInTimeLine in timeLine)
                {
                    TimeLine.Add(eventInTimeLine);
                }
            }
        }
    }
}