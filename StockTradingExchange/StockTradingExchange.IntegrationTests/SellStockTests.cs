using Microsoft.Extensions.Configuration;
using StockTradingExchange.Application.Stocks.SellStocks;
using StockTradingExchange.Infrastructure;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StockTradingExchange.IntegrationTests
{
    public class SellStockTests:IClassFixture<TestBaseFixture>
    {
        TestBaseFixture _testBase;
        public SellStockTests(TestBaseFixture testBase)
        {
            _testBase = testBase;
        }

        [Fact]
        public async Task SellStocksTest()
        {
            _testBase.BeforeEachTest();
            const int customerId = 1;
            const string name = "Test";
            const int quantity = 1;
            const double price = 100.2;
            const string stockType = "Market";
            var customer = await CommandsExecutor.Execute(new SellStockCommand(customerId, name, quantity, price, stockType));
            Assert.NotNull(customer);
            Assert.Equal(customer.Name, name);
            Assert.Equal(customer.Quantity, quantity);
        }
    }
}
