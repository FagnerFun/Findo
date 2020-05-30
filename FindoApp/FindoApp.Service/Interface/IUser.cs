using FindoApp.Domain.Model;
using Refit;
using System.Threading.Tasks;

namespace FindoApp.Service.Interface
{
    [Headers("Content-Type: application/json")]
    public interface IUser
    {
        [Get("/api/Authenticate/{user}/{password}")]
        Task<User> Authenticate(string user, string password);
    }
}
