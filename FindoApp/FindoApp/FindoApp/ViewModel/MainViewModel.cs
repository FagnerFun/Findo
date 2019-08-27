using System;
using System.Collections.Generic;
using System.Text;
using Prism.Navigation;

namespace FindoApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
