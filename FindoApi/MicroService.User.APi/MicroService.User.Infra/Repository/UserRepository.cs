using Findo.Framework.Interface.Interface;
using Findo.Framework.Persistence.Repositories;
using MicroService.User.Domain.Interface.Repository;

namespace MicroService.User.Infra.Repository
{
    public class UserRepository : GenericRepository<Domain.Model.User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
