using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StockTradingExchange.Application.Configuration;
using System;
using StockTradingExchange.Infrastructure.Database;
using CommonServiceLocator;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.AspNetCore.Http;

namespace StockTradingExchange.Infrastructure
{
    public class ApplicationStartup
    {
        public static IServiceProvider Initialize(
            IServiceCollection services,
            string connectionString,
            ILogger logger,
            IHttpContextAccessor executionContextAccessor)
        {
            var serviceProvider = CreateAutofacServiceProvider(
                services,
                connectionString,
                logger,
                executionContextAccessor);

            return serviceProvider;
        }

        private static IServiceProvider CreateAutofacServiceProvider(
            IServiceCollection services,
            string connectionString,
            ILogger logger,
            IHttpContextAccessor executionContextAccessor)
        {
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new LoggingModule(logger));
            container.RegisterModule(new DataAccessModule(connectionString));
            container.RegisterModule(new MediatorModule());
            container.RegisterInstance(executionContextAccessor);

            var buildContainer = container.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

            var serviceProvider = new AutofacServiceProvider(buildContainer);

            CompositionRoot.SetContainer(buildContainer);

            return serviceProvider;
        }
    }
}