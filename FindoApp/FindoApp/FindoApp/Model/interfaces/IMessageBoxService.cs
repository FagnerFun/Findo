using System;
using System.Threading.Tasks;

namespace FindoApp.Model.interfaces
{
    public interface IMessageBoxService
    {
        void ShowAlert(string title, string message, string ok, Action onClosed = null);
        Task<string> ShowActionSheet(string title, string cancel, string destruction, string[] buttons = null);
    }
}
