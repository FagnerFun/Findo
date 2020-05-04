using Findo.Framework.Service.Service;
using MicroService.CheckList.Domain.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.CheckList.Service
{
    public class CheckListService : GenericService<IUserRepository, CheckList>, IUserService
    {
        IConfiguration _config;

        public CheckListService(
            IUnitOfWork unitOfWork,
            IUserRepository repository,
            IConfiguration configuration,
            ILogger<UserService> logger) : base(unitOfWork, repository, logger)
        {
            _config = configuration;
        }

    }
}
