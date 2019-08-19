using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Findo.Framework.API.Controller;
using MicroService.User.Domain.Interface.Service;
using MicroService.User.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.User.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<Domain.Model.User, IUserService>
    {
        public UserController(IUserService service): base(service)
        {
        }

        public override ActionResult<IEnumerable<Domain.Model.User>> Get()
        {
            return new List<MicroService.User.Domain.Model.User>()
            {
                new Domain.Model.User
                {
                    Id = 4,
                    FacebookId = string.Empty,
                    GoogleId = string.Empty,
                    Mail = "fagner.muniz@lobu.com.br",
                    Password = "1234"
                }
            };
        }
    }
}