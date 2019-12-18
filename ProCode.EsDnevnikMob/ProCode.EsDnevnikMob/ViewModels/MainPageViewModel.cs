using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProCode.EsDnevnik.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Elektronski dnevnik";
            this.dialogService = dialogService;
        }

        private IPageDialogService dialogService;

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private DelegateCommand navigateCommand;
        public DelegateCommand NavigateCommand => navigateCommand ?? (navigateCommand = new DelegateCommand(ExecuteNavigateCommand));
        async void ExecuteNavigateCommand()
        {
            if (string.IsNullOrWhiteSpace(_username))
            {
                await dialogService.DisplayAlertAsync("Greška?", "Unesi korisničko ime.", "Uredu");
                return;
            }
            if (string.IsNullOrWhiteSpace(_password))
            {
                await dialogService.DisplayAlertAsync("Greška?", "Unesi lozinku.", "Uredu");
                return;
            }


            var securePassword = new System.Security.SecureString();
            foreach (var c in _password)
            {
                securePassword.AppendChar(c);
            }
            var esdService = new EsDnevnik.Service.EsDnevnik(new UserCredential(_username, securePassword));
            try
            {
                await esdService.LoginAsync();
                var param = new NavigationParameters();
                param.Add(nameof(esdService), esdService);
                await NavigationService.NavigateAsync("StudentListPage", param);
            }
            catch (Exception ex)
            {
                await dialogService.DisplayAlertAsync("Greška?", ex.Message, "Uredu");
            }
        }
    }
}
