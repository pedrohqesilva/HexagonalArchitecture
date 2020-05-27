using ConsoleAppRE;
using Example.Domain.Commands;
using Example.Domain.CommandsHandler;
using Example.Domain.Interfaces.Queries;
using Example.Domain.Interfaces.Repositories;
using Example.Domain.Queries;
using Example.Infrastructure.Data.Context;
using Example.Infrastructure.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Infrastructure.CrossCutting.IoC
{
    public static class InjectorContainer
    {
        public static void Register(IServiceCollection services, string connectionString)
        {
            Core.Infrastructure.CrossCutting.IoC.InjectorContainer.InjectContext<ExampleContext>(services, connectionString);

            services.AddScoped<ICountryQuery, CountryQuery>();
            services.AddScoped<IRegionQuery, RegionQuery>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();

            services.AddScoped<IRequestHandler<AddCountryCommand, Countries>, AddCountryCommandHandler>();
        }
    }
}