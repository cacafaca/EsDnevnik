﻿using Prism;
using Prism.Ioc;
using ProCode.EsDnevnikMob.ViewModels;
using ProCode.EsDnevnikMob.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProCode.EsDnevnikMob
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            try
            {
                InitializeComponent();

                await NavigationService.NavigateAsync("NavigationPage/MainPage");
            }
            catch (System.Exception ex)
            {
                MainPage = new ErrorPage();
                ((ErrorPageViewModel)MainPage.BindingContext).Message = ex.Message;
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<StudentListPage, StudentListPageViewModel>();
            containerRegistry.RegisterForNavigation<StudentOverviewPage, StudentOverviewPageViewModel>();
            containerRegistry.RegisterForNavigation<ErrorPage, ErrorPageViewModel>();
        }
    }
}
