using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using StockTradingExchange.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

namespace StockTradingExchange.IntegrationTests
{
    public class TestBaseFixture : IDisposable
    {
        protected string ConnectionString;

        public TestBaseFixture()
        {
            this.ConnectionString = "test";
        }
        public void BeforeEachTest()
        {
            var executionContext = new HttpContextAccessor();
           
            ApplicationStartup.Initialize(
                new ServiceCollection(),
                ConnectionString,
                Logger.None,
                executionContext);
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}