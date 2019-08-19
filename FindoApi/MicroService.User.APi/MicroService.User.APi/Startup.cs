using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Findo.Framework.API.Middleware;
using Findo.Framework.Cache;
using Findo.Framework.Cache.DependencyInjection;
using Findo.Framework.Interface.Interface;
using Findo.Framework.Persistence.UnitOfWork;
using FluentValidation.AspNetCore;
using MicroService.User.APi.Controllers;
using MicroService.User.Domain.Interface.Repository;
using MicroService.User.Domain.Interface.Service;
using MicroService.User.Infra.Data;
using MicroService.User.Infra.Repository;
using MicroService.User.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace MicroService.User.APi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(UserController));

            services.AddMvc()
                    .AddFluentValidation()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddApplicationPart(assembly).AddControllersAsServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "User", Version = "v1" });
            });

            ConfigureDependencyInject(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddSerilog();
            app.UseHandleExceptionMiddleware();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Micro Service - User");
            });


            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureDependencyInject(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork>(x => new UnitOfWork<UserContext>());
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IUserService), typeof(UserService));

            services.AddNullCache();

        }
    }
}
