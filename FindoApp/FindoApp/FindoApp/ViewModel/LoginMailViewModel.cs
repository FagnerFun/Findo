using FindoApp.Domain.Model;
using FindoApp.Model.interfaces;
using FindoApp.Service.Interface;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindoApp.ViewModel
{
    public class LoginMailViewModel : ViewModelBase
    {
        private IUser _userapi;
        private INavigationService _navigationService;
        private IMessageBoxService _messageBoxService;

        public string Mail { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; set; }


        public LoginMailViewModel(INavigationService navigationService, IUser userapi, IMessageBoxService messageBoxService) : base(navigationService)
        {
            _userapi = userapi;
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;

            LoginCommand = new Command(async () => await LoginCommandExecute());
        }

        private async Task LoginCommandExecute()
        {
            try
            {
                var user = await _userapi.Authenticate(Mail, Password);
                if (user != null)
                    User.AccessToken = user.Token;

                await _navigationService.SelectTabAsync(nameof(View.CheckListPage));
            }
            catch(Exception ex)
            {
                _messageBoxService.ShowAlert("Error", "Login fail. Please try later", "Ok");
                Console.WriteLine(ex.Message);
                await _navigationService.GoBackAsync();
            }
        }
    }
}
