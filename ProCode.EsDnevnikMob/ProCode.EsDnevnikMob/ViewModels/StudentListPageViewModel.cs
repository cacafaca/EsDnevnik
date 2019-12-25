using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProCode.EsDnevnik.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class StudentListPageViewModel : ViewModelBase, INavigatedAware
    {
        public StudentListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Moja deca";
            students = new ObservableCollection<Student>();
        }

        EsDnevnik.Service.EsDnevnik esdService = null;

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (esdService == null)
                esdService = parameters.GetValue<EsDnevnik.Service.EsDnevnik>("esdService");

            // Skip if already fetched.
            if (Students.Count == 0)
            {
                IList<Student> students = null;
#if !DEBUGFAKE
                students = await esdService.GetStudentsAsync();
#else
                await Task.Run(() => { students = esdService.GetStudentsFake(); });
#endif
                foreach (var stud in students)
                {
                    Students.Add(stud);
                }
            }
        }

        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get { return students; }
            set { SetProperty(ref students, value); }
        }

        private Student selectedStudent;

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; }
        }

        private DelegateCommand itemTappedCommand;
        public DelegateCommand ItemTappedCommand => itemTappedCommand ?? (itemTappedCommand = new DelegateCommand(ExecuteItemTappedCommand));

        private async void ExecuteItemTappedCommand()
        {
            var param = new NavigationParameters();
            param.Add(nameof(esdService), esdService);
            param.Add(nameof(SelectedStudent), SelectedStudent);
            await NavigationService.NavigateAsync(nameof(Views.StudentOverviewPage), param);
        }

        public static string GetEsdServiceParamName()
        {
            return nameof(esdService);
        }
    }
}