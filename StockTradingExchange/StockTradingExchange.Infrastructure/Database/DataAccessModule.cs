using Autofac;
using Microsoft.EntityFrameworkCore;
using StockTradingExchange.Application.Configuration.Data;
using StockTradingExchange.Domain.SeedWork;
using StockTradingExchange.Domain.StockTrading;
using StockTradingExchange.Infrastructure.Domain;
using StockTradingExchange.Infrastructure.Domain.StockTrading;

namespace StockTradingExchange.Infrastructure.Database
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public DataAccessModule(string databaseConnectionString)
        {
            this._databaseConnectionString = databaseConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();


            builder.RegisterType<CustomerTradingRepository>()
                .As<ICustomerTradingRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerQuotesRepository>()
                .As<ICustomerQuotesRepository>()
                .InstancePerLifetimeScope();

            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<CustomerTradingContext>();
                    dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);
                    return new CustomerTradingContext(dbContextOptionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }
    }
}