using FindoApp.Model.interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindoApp.Services
{
    class MessageBoxService : IMessageBoxService
    {
        private static Page CurrentMainPage { get { return Application.Current.MainPage; } }

        public async void ShowAlert(string title, string message, string ok, Action onClosed = null)
        {
            await CurrentMainPage.DisplayAlert(title, message, ok);
            onClosed?.Invoke();
        }

        public async Task<string> ShowActionSheet(string title, string cancel, string destruction = null, string[] buttons = null)
        {
            var displayButtons = buttons ?? new string[] { };
            var action = await CurrentMainPage.DisplayActionSheet(title, cancel, destruction, displayButtons);
            return action;
        }
    }
}