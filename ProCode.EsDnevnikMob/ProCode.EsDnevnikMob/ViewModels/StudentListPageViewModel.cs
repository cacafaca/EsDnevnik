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
            Task<IList<Student>> studentsTask = esdService.GetStudentsAsync();
            Students = studentsTask.Result;
        }

        public IList<EsDnevnik.Model.Student> Students { get; private set; }
    }
}
