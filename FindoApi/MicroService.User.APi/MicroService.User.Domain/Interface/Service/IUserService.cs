using Findo.Framework.Interface.Interface;
using System.Threading.Tasks;

namespace MicroService.User.Domain.Interface.Service
{
    public interface IUserService : IService<Model.User>
    {
        Task<dynamic> LoginAsync(Model.User login);
    }
}
