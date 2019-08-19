using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Findo.Framework.Interface.Interface;

namespace Findo.Framework.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TModel, TService> : ControllerBase
       where TModel : class
       where TService : IService<TModel>
    {
        protected TService _service;
        /// <summary>
        /// 
        /// </summary>
        protected BaseController(TService service)
        {
            this._service = service;
        }
        [HttpGet("[action]/{id}")]
        public virtual ActionResult<TModel> Get(string id)
        {
            return this._service.Retrieve(new Guid(id));

        }
        [HttpGet]
        public virtual ActionResult<IEnumerable<TModel>> Get()
        {
            return _service.Retrieve().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult<TModel> Insert(TModel model)
        {
            _service.Create(model);
            return model;
        }
        [HttpPut]
        public virtual ActionResult<TModel> Update(TModel model)
        {
            _service.Update(model);
            return model;
        }
        [HttpDelete]
        public virtual ActionResult<TModel> Delete(TModel model)
        {
            _service.Delete(model);
            return model;
        }
    }
}
