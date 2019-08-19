using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace FindoApp.Model.interfaces
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(MobileServiceAuthenticationProvider provider);
    }
}
