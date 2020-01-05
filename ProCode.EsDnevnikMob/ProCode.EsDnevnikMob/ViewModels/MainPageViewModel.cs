﻿using Prism.Commands;
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
            Title = "Електронски дневник";
            this.dialogService = dialogService;

            IsLogging = false;
            Username = userSettings.GetUsernameAsync().Result;
            Password = userSettings.GetPasswordAsync().Result;
        }

        private readonly IPageDialogService dialogService;
        private readonly UserSettings userSettings = new UserSettings();
        private EsDnevnik.Service.EsDnevnik esdService;
        private bool firstAppaerance = true;

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

        private DelegateCommand loginNavigateCommand;
        public DelegateCommand LoginNavigateCommand => loginNavigateCommand ?? (loginNavigateCommand = new DelegateCommand(ExecuteLoginNavigateCommand));
        async void ExecuteLoginNavigateCommand()
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

            if (esdService == null)
                esdService = new EsDnevnik.Service.EsDnevnik(new UserCredential(username, securePassword));

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

                var param = new NavigationParameters
                {
                    { nameof(esdService), esdService }
                };
                await NavigationService.NavigateAsync(nameof(Views.StudentListPage), param);
                IsLogging = false;
            }
            catch (Exception ex)
            {
                await dialogService.DisplayAlertAsync("Greška?", ex.Message, "Uredu");
            }
            finally
            {
                IsLogging = false;
            }
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

        private DelegateCommand appearingCommand;
        public DelegateCommand AppearingCommand => appearingCommand ?? (appearingCommand = new DelegateCommand(ExecuteAppearingCommand));

        private void ExecuteAppearingCommand()
        {
            if (firstAppaerance && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                firstAppaerance = false;
                ExecuteLoginNavigateCommand();
            }
        }

    }
}