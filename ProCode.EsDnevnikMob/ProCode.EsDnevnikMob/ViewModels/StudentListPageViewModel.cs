using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProCode.EsDnevnik.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class StudentListPageViewModel : ViewModelBase, INavigatedAware
    {
        public StudentListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Moja deca";
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var esdService = parameters.GetValue<EsDnevnik.Service.EsDnevnik>("esdService");
            //Task<IList<Student>> studentsTask = esdService.GetStudentsAsync();
            //Students = studentsTask.Result;
            Students = new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    FullName = "Tea",
                    Jmbg = "123",
                    Gender = "F"
                },
                new Student()
                {
                    Id = 2,
                    FullName = "Stefa",
                    Jmbg = "456",
                    Gender = "M"
                }
            };
        }

        private IList<EsDnevnik.Model.Student> _students;
        public IList<EsDnevnik.Model.Student> Students
        {
            get { return _students; }
            set
            {
                SetProperty(ref _students, value);
            }
        }
    }
}