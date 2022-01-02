using Microsoft.Extensions.Configuration;
using StockTradingExchange.Application.Stocks.BuyStocks;
using StockTradingExchange.Infrastructure;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StockTradingExchange.IntegrationTests
{
    public class BuyStockTests : IClassFixture<TestBaseFixture>
    {
        TestBaseFixture _testBase;

        public BuyStockTests(TestBaseFixture testBase)
        {
            _testBase = testBase;
        }

        [Fact]
        public async Task BuyStocksTest()
        {
            _testBase.BeforeEachTest();
            const int customerId = 1;
            const string name = "Test";
            const int quantity = 1;
            const double price = 100.2;
            const string stockType = "Market";

            var customer = await CommandsExecutor.Execute(new BuyStockCommand(customerId, name, quantity, price, stockType));

            Assert.NotNull(customer);
            Assert.Equal(customer.Name, name);
            Assert.Equal(customer.Quantity, quantity);
        }
    }
}
