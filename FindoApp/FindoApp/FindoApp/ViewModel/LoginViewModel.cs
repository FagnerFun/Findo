using FindoApp.View;
using Microsoft.WindowsAzure.MobileServices;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindoApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        public ICommand MailCommand { get; set; }
        public ICommand FacebookComannd { get; set; }
        public ICommand GoogleComannd { get; set; }

        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
            MailCommand = new Command(async () => await Mail());
            FacebookComannd = new Command(async () => await Facebook_Execute());
            GoogleComannd = new Command(async () => await Google_Execute());
        }

        private Task Mail()
        {
            return NavigationService.NavigateAsync("LoginMailPage");
        }

        private async Task Facebook_Execute()
        {
            if (await App.Authenticator.Authenticate(MobileServiceAuthenticationProvider.Facebook))
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}?selectedTab={nameof(CheckListPage)}");
            }
        }

        private async Task Google_Execute()
        {
            if (await App.Authenticator.Authenticate(MobileServiceAuthenticationProvider.Google))
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}?selectedTab={nameof(CheckListPage)}");
            }
        }

    }
}
