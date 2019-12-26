using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProCode.EsDnevnik.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Elektronski dnevnik";
            this.dialogService = dialogService;

            IsLogging = false;
            Username = userSettings.GetUsernameAsync().Result;
            Password = userSettings.GetPasswordAsync().Result;
        }

        private IPageDialogService dialogService;
        private UserSettings userSettings = new UserSettings();

        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private DelegateCommand navigateCommand;
        public DelegateCommand NavigateCommand => navigateCommand ?? (navigateCommand = new DelegateCommand(ExecuteNavigateCommand));
        async void ExecuteNavigateCommand()
        {

            if (string.IsNullOrWhiteSpace(Username))
            {
                await dialogService.DisplayAlertAsync("Greška?", "Unesi korisničko ime.", "Uredu");
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                await dialogService.DisplayAlertAsync("Greška?", "Unesi lozinku.", "Uredu");
                return;
            }

            var securePassword = new System.Security.SecureString();
            foreach (var c in Password)
            {
                securePassword.AppendChar(c);
            }
            var esdService = new EsDnevnik.Service.EsDnevnik(new UserCredential(username, securePassword));

            try
            {
                IsLogging = true;
#if !DEBUGFAKE
                await esdService.LoginAsync();
#else
#endif
                // Store user credentials.
                await userSettings.SetUsernameAsync(Username);
                await userSettings.SetPasswordAsync(Password);

                var param = new NavigationParameters();
                param.Add(nameof(esdService), esdService);
                await NavigationService.NavigateAsync(nameof(Views.StudentListPage), param);
                IsLogging = false;
            }
            catch (Exception ex)
            {
                IsLogging = false;
                await dialogService.DisplayAlertAsync("Greška?", ex.Message, "Uredu");
            }



        }

        private bool isLogging;
        public bool IsLogging
        {
            get { return isLogging; }
            set { SetProperty(ref isLogging, value); }
        }
    }
}
