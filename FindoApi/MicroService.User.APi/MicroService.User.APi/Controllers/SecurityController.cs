using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Findo.Framework.Interface.Exceptions;
using MicroService.User.APi.SwaggerExamples;
using MicroService.User.Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Examples;

namespace MicroService.User.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        /// <summary>
        /// Using for all services login
        /// </summary>
        private readonly IUserService _service;

        public SecurityController(IUserService service)
        {
            this._service = service;
        }

        /// <summary>
        ///  Login in system
        /// </summary>
        /// <param name="login">data login</param>
        /// <returns> Ok</returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerRequestExample(typeof(Domain.Model.User), typeof(UserExample))]
        public async Task<ActionResult> LoginAsync([FromBody]Domain.Model.User login)
        {
            if (this.ModelState.IsValid)
            {
                var token = await this._service.LoginAsync(login);
                return this.Ok(token);
            }

            throw new BusinessException(MessagesErrorsModel(this.ModelState));
        }


        /// <summary>
        /// return errors of the model
        /// </summary>
        /// <param name="modelstate"> Represents the state of an attempt to bind values from an HTTP Request to an action method, which includes validation information.</param>
        /// <returns> String errors</returns>
        private static string MessagesErrorsModel(ModelStateDictionary modelstate)
        {
            if (modelstate is null)
            {
                throw new BusinessException("Invalid model");
            }

            string messages = string.Join("; ", modelstate.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            return messages;
        }
    }
}