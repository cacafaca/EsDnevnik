using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ProCode.EsDnevnik.Service;
using System;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Електронски дневник";

            IsLogging = false;

            try
            {
                Username = userSettings.GetUsernameAsync().Result;
                Password = userSettings.GetPasswordAsync().Result;
            }
            catch (Exception ex)
            {
                DisplayAlertAsync(ex).Wait();
            }
        }

        private readonly UserSettings userSettings = new UserSettings();
        private EsDnevnik.Service.EsDnevnik esdService;
        private bool autoLoginFlag = true;

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

        private bool isLogging;
        public bool IsLogging
        {
            get { return isLogging; }
            set
            {
                SetProperty(ref isLogging, value);
                if (isNotLogging == isLogging)
                    IsNotLogging = !isLogging;
            }
        }

        private bool isNotLogging;
        public bool IsNotLogging
        {
            get { return isNotLogging; }
            set
            {
                SetProperty(ref isNotLogging, value);
                if (isLogging == isNotLogging)
                    IsLogging = !isNotLogging;
            }
        }

        int failedLoginAttempts = 0;
        private int FailedLoginAttempts
        {
            get { return failedLoginAttempts; }
            set
            {
                failedLoginAttempts = value;
                IsVisibleFailedLoginInstruction = failedLoginAttempts > 0;
            }
        }

        bool isVisibleFailedLoginInstruction;
        public bool IsVisibleFailedLoginInstruction
        {
            get { return isVisibleFailedLoginInstruction; }
            private set { SetProperty(ref isVisibleFailedLoginInstruction, value); }
        }

        #region Commands

        private DelegateCommand loginNavigateCommand;
        public DelegateCommand LoginNavigateCommand => loginNavigateCommand ?? (loginNavigateCommand = new DelegateCommand(ExecuteLoginNavigateCommandAsync));
        async void ExecuteLoginNavigateCommandAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    await DialogService.DisplayAlertAsync("Грешка?", "Унеси корисничко име.", "У реду");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    await DialogService.DisplayAlertAsync("Грешка?", "Унеси лозинку.", "У реду");
                    return;
                }

                var securePassword = new System.Security.SecureString();
                foreach (var c in Password)
                {
                    securePassword.AppendChar(c);
                }

                if (esdService == null)
                    esdService = new EsDnevnik.Service.EsDnevnik(new UserCredential(username, securePassword));
                else
                    esdService.SetUserCredentials(new UserCredential(username, securePassword));

                IsLogging = true;
#if !DEBUGFAKE
                await esdService.LoginAsync();
#else
#endif
                FailedLoginAttempts = 0;

                // Store user credentials.
                await userSettings.SetUsernameAsync(Username);
                await userSettings.SetPasswordAsync(Password);

                var param = new NavigationParameters
                {
                    { nameof(esdService), esdService }
                };
                await NavigationService.NavigateAsync(nameof(Views.StudentListPage), param);
                IsLogging = false;
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(ex);
                FailedLoginAttempts++;
            }
            finally
            {
                IsLogging = false;
            }
        }

        private DelegateCommand appearingCommand;
        public DelegateCommand AppearingCommand => appearingCommand ?? (appearingCommand = new DelegateCommand(ExecuteAppearingCommand));

        private void ExecuteAppearingCommand()
        {
            try
            {
                if (autoLoginFlag && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    autoLoginFlag = false;
                    ExecuteLoginNavigateCommandAsync();
                }
            }
            catch (Exception ex)
            {
                DisplayAlertAsync(ex).Wait();
            }
        }

        private DelegateCommand openBrowserCommand;
        public DelegateCommand OpenBrowserCommand => openBrowserCommand ?? (openBrowserCommand = new DelegateCommand(ExecuteOpenBrowserCommandAsync));
        async void ExecuteOpenBrowserCommandAsync()
        {
            await Xamarin.Essentials.Browser.OpenAsync(new EsDnevnik.Service.UriDictionary().GetBase(), Xamarin.Essentials.BrowserLaunchMode.SystemPreferred);
        }

        #endregion
    }
}