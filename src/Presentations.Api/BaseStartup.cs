using AutoMapper;
using Core.Infrastructure.CrossCutting.Environment;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Presentations.Api.Conventions;
using Presentations.Api.Extensions;
using System.Globalization;

namespace Presentations.Api
{
    public class BaseStartup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger<BaseStartup> _logger;

        public BaseStartup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            ConfigureCultureInfo();

            services
                .AddMvc(opts =>
                {
                    opts.Conventions.Add(new CommaSeparatedRouteConvention());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            RegisterContainers(services);
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionMiddleware(_logger);
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        protected static void ConfigureCultureInfo()
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        protected void RegisterContainers(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(new[] {
                    "Presentations.Api"
                });
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            mappingConfig.AssertConfigurationIsValid();

            services.AddMediatR(typeof(Startup));

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var settings = Configuration.GetSection(@"Services").Get<AppSetttings>();

            Core.Infrastructure.CrossCutting.IoC.InjectorContainer.Register(services, settings, connectionString);
            Example.Infrastructure.CrossCutting.IoC.InjectorContainer.Register(services, connectionString);
        }
    }
}