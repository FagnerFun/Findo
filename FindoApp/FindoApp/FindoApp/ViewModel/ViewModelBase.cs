﻿using Prism;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindoApp.ViewModel
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IActiveAware
    {
        public ICommand TitleCommand { get; }
        protected ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            TitleCommand = new Command<object>(TitleCommandEvent);
        }

        public INavigationService NavigationService { get; }

        private bool isBusy;

        public event EventHandler IsActiveChanged;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public bool IsNotBusy => !IsBusy;

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        public virtual void TitleCommandEvent(object e)
        {
            return;
        }

        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
        protected async Task ExecuteBusyAction(Func<Task> theBusyAction)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await theBusyAction();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }
        public virtual void OnNavigatedTo(INavigationParameters parameters) { }
        public virtual void OnNavigatingTo(INavigationParameters parameters) { }
    }
}
