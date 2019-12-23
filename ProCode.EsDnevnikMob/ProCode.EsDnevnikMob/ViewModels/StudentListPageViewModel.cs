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

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        EsDnevnik.Service.EsDnevnik esdService = null;

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            esdService = parameters.GetValue<EsDnevnik.Service.EsDnevnik>("esdService");

            var students = await esdService.GetStudentsAsync();
            //var students = esdService.GetStudentsFakeAsync();
            foreach (var stud in students)
            {
                Students.Add(stud);
            }
        }

        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                SetProperty(ref students, value);
            }
        }

        private Student selectedStudent;

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; }
        }

    }
}