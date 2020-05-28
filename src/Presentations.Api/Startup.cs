using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Presentations.Api.Extensions;

namespace Presentations.Api
{
    public class Startup : BaseStartup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
            : base(configuration, logger)
        {
            Configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.UseSwaggerMiddleware();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            ConfigureSwagger(app);
            base.Configure(app, env);
        }

        private static void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseOpenApi()
               .UseSwaggerUi3()
               .UseReDoc(x =>
               {
                   x.Path = "/ReDoc";
               });
        }
    }
}