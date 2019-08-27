using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindoApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        public ICommand MailCommand;

        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
            MailCommand = new Command(async () => await Mail());
        }

        private Task Mail()
        {
            return NavigationService.NavigateAsync("MainPage");
        }
    }
}
