using Core.Domain.EventHandlers;
using Core.Domain.Events;
using Core.Domain.Interfaces;
using Core.Infrastructure.CrossCutting.Bus;
using Core.Infrastructure.CrossCutting.Environment;
using Core.Infrastructure.Data.Context;
using Core.Infrastructure.Data.Repository.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Core.Infrastructure.CrossCutting.IoC
{
    public static class InjectorContainer
    {
        public static void Register(IServiceCollection services, AppSetttings appSetttings, string connectionString)
        {
            services.AddScoped((x) => appSetttings);
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            ConfigureDbOptions(services, connectionString);
            InjectContext<EventStoreContext>(services, connectionString);
        }

        private static void ConfigureDbOptions(IServiceCollection services, string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                AddConnection(services, connectionString);
                AddTransaction(services);
                AddDbOptions(services);
            }
        }

        private static void AddConnection(IServiceCollection services, string connectionString)
        {
            services.AddScoped((serviceProvider) =>
            {
                var dbConnection = new OracleConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            });
        }

        private static void AddTransaction(IServiceCollection services)
        {
            services.AddScoped<DbTransaction>((serviceProvider) =>
            {
                var dbConnection = serviceProvider.GetService<OracleConnection>();
                return dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            });
        }

        private static void AddDbOptions(IServiceCollection services)
        {
            services.AddScoped((serviceProvider) =>
            {
                var dbConnection = serviceProvider.GetService<OracleConnection>();

                var dbContext = new DbContextOptionsBuilder()
                    .UseOracle(dbConnection, x =>
                    {
                        x.MigrationsHistoryTable("MIGRACOES");
                        //x.UseOracleSQLCompatibility("11");
                        x.MigrationsAssembly("Core.Infrastructure.Data.Migrations");
                    })
                    .UseLazyLoadingProxies()
                    .EnableSensitiveDataLogging();

                AddLogContext(dbContext);
                return dbContext.Options;
            });
        }

        public static void InjectContext<TContext>(IServiceCollection services, string connectionString) where TContext : BaseContext
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                UserInMemoryDatabase<TContext>(services);
            }
            else
            {
                UseOracleDatabase<TContext>(services);
            }
        }

        private static void UserInMemoryDatabase<TContext>(IServiceCollection services) where TContext : BaseContext
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddEntityFrameworkProxies()
                .BuildServiceProvider();

            services.AddDbContext<TContext>(options => options
                .UseInternalServiceProvider(serviceProvider)
                .UseInMemoryDatabase("SGAP")
                .UseLazyLoadingProxies()
                .EnableSensitiveDataLogging());
        }

        private static void UseOracleDatabase<TContext>(IServiceCollection services) where TContext : BaseContext
        {
            services.AddScoped((serviceProvider) =>
            {
                var transaction = serviceProvider.GetService<DbTransaction>();
                var options = serviceProvider.GetService<DbContextOptions>();

                var context = (TContext)Activator.CreateInstance(typeof(TContext), options);
                context.Database.UseTransaction(transaction);
                return context;
            });
        }

        private static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));

            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }

        [Conditional("DEBUG")]
        private static void AddLogContext(DbContextOptionsBuilder dbContext)
        {
            dbContext.UseLoggerFactory(GetLoggerFactory());
            //.EnableSensitiveDataLogging(); //Habilitar para validações do Context
        }
    }
}