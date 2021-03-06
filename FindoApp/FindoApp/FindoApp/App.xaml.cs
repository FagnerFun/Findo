﻿using DryIoc.Messages;
using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model.Const;
using FindoApp.Model.interfaces;
using FindoApp.Service;
using FindoApp.Service.Interface;
using FindoApp.Services;
using FindoApp.View;
using FindoApp.ViewModel;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Refit;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FindoApp
{
    public partial class App : PrismApplication
    {
        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }
        
        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}?selectedTab={nameof(CheckListPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<CheckListPage, CheckListViewModel>();
            containerRegistry.RegisterForNavigation<CheckListItemPage, CheckListItemViewModel>();
            containerRegistry.RegisterForNavigation<LoginMailPage, LoginMailViewModel>();

            containerRegistry.Register<ICheckListService, CheckListService>();

            containerRegistry.RegisterSingleton<IMessageBoxService, MessageBoxService>();

            #region refit

            var client = new HttpClient()
            {
                BaseAddress = new Uri(AppSettings.ApiUrl),
                Timeout = TimeSpan.FromSeconds(90)
            };

            containerRegistry.RegisterInstance<IFindo>(RestService.For<IFindo>(client));


            var clientUser = new HttpClient()
            {
                BaseAddress = new Uri(AppSettings.ApiUserUrl),
                Timeout = TimeSpan.FromSeconds(90)
            };

            containerRegistry.RegisterInstance<IUser>(RestService.For<IUser>(clientUser));

            #endregion
        }
    }
}
