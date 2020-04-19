using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Findo.Framework.API.Middleware;
using Findo.Framework.Cache.DependencyInjection;
using Findo.Framework.Interface.Interface;
using Findo.Framework.Persistence.UnitOfWork;
using FluentValidation.AspNetCore;
using MicroService.User.APi.Controllers;
using MicroService.User.APi.Security;
using MicroService.User.APi.SwaggerFilter;
using MicroService.User.Domain.Interface.Repository;
using MicroService.User.Domain.Interface.Service;
using MicroService.User.Infra.Data;
using MicroService.User.Infra.Repository;
using MicroService.User.Service.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

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

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddCors(o => o.AddPolicy("User", builder =>
            {
                builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader();
            }));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                                     {
                                         options.TokenValidationParameters = new TokenValidationParameters()
                                         {
                                             IssuerSigningKey = Audience.Secret,
                                             ValidAudience = Audience.Aud,
                                             ValidIssuer = Audience.Iss,
                                             ValidateIssuerSigningKey = true,
                                             ValidateLifetime = true,
                                             ClockSkew = TimeSpan.FromMinutes(0)
                                         };
                                     });


            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<LowercaseDocumentFilter>();
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "User",
                    Description = "Expose User interfaces",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Fagner Muniz",
                        Email = "fagner.muniz@lobu.com.br",
                        Url = UriHelper.Encode(new Uri("http://www.lobu.com"))
                    }
                });

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "Bearer", new string[]
                        {
                        }
                    },
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.OperationFilter<ExamplesOperationFilter>();
                c.AddSecurityRequirement(security);

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MicroService.User.APi.xml");
                c.IncludeXmlComments(filePath);
                c.DescribeAllEnumsAsStrings();
            });
        }

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "User Interfaces API";
                c.DocExpansion(DocExpansion.None);
            });


            app.UseStaticFiles();
            app.UseCors("User");
            app.UseAuthentication();
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
