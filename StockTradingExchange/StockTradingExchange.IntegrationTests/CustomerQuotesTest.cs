using Microsoft.Extensions.Configuration;
using StockTradingExchange.Application.Stocks.SendQuotes;
using StockTradingExchange.Infrastructure;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StockTradingExchange.IntegrationTests
{
    public class CustomerQuotesTest : IClassFixture<TestBaseFixture>
    {
        TestBaseFixture _testBase;
        public CustomerQuotesTest(TestBaseFixture testBase) 
        {
            _testBase = testBase;
        }

        [Fact]
        public async Task SendQuotesRequestTest()
        {
            _testBase.BeforeEachTest();
            const int customerId = 1;
            const string quoteName = "test quote";
            DateTime createdDate = DateTime.Now;
            const int quantity = 1;
            const double price = 100.1;
            var customer = await CommandsExecutor.Execute(new SendQuotesCommand(customerId, quoteName, createdDate, quantity, price));

            Assert.NotNull(customer);
            Assert.Equal(customer.QuoteName, quoteName);
            Assert.Equal(customer.Quantity, quantity);
            Assert.Equal(customer.Price, price);
            Assert.Equal(customer.CreatedDate, createdDate);
        }
    }
}
