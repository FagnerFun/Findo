using Findo.Framework.Interface.Interface;
using Findo.Framework.Service.Service;
using MicroService.User.Domain.Interface.Repository;
using MicroService.User.Domain.Interface.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MicroService.User.Service.Service
{
    public class UserService: GenericService<IUserRepository, Domain.Model.User>, IUserService
    {
        IConfiguration _config;
        
        public UserService(
            IUnitOfWork unitOfWork,
            IUserRepository repository,
            IConfiguration configuration,
            ILogger<UserService> logger) : base(unitOfWork, repository, logger)
        {
            _config = configuration;
        }

        public System.Threading.Tasks.Task<dynamic> LoginAsync(Domain.Model.User login)
        {
            throw new System.NotImplementedException();
        }
    }
}
