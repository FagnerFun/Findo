using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindoApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
    {
        //bool authenticated = false;
        public LoginPage ()
		{
            InitializeComponent();
        }

        //private async void Facebook_Clicked(object sender, EventArgs e)
        //{
        //    if (await App.Authenticator.Authenticate(MobileServiceAuthenticationProvider.Facebook))
        //    {
        //        Application.Current.MainPage = new MainPage();
        //    }
        //}

        //private async void Google_Clicked(object sender, EventArgs e)
        //{
        //    if (await App.Authenticator.Authenticate(MobileServiceAuthenticationProvider.Google))
        //    {
        //        Application.Current.MainPage = new MainPage();
        //    }
        //}

        //private void Mail_Clicked(object sender, EventArgs e)
        //{
        //    Application.Current.MainPage = new MainPage();
        //}
    }
}