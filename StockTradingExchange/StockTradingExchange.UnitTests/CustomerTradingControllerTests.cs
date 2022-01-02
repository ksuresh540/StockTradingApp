using MediatR;
using Moq;
using StockTradingExchange.API.Controllers;
using StockTradingExchange.Application.Stocks.BuyStocks;
using StockTradingExchange.Application.Stocks.SellStocks;
using StockTradingExchange.Application.Stocks.SendQuotes;
using StockTradingExchange.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StockTradingExchange.UnitTests
{
    public class CustomerTradingControllerTests
    {
        [Fact]
        public async Task BuyStocks_ReturnSuccess()
        {
            var customerTrading = new CustomerTrading(1, "test", 1, 100, "Market", 100);
            var mockMediator = new Mock<IMediator>();
            var controller = new CustomerTradingController(mockMediator.Object);
            mockMediator.Setup(x => x.Send(It.IsAny<BuyStockCommand>(), default(CancellationToken))).ReturnsAsync(customerTrading);
            var response = await controller.BuyStocks(It.IsAny<BuyStockRequest>());
            Assert.NotNull(response);
        }

        [Fact]
        public async Task SellStocks_ReturnSuccess()
        {
            var customerTrading = new CustomerTrading(1, "test", 1, 100, "Market", 100);
            var mockMediator = new Mock<IMediator>();
            var controller = new CustomerTradingController(mockMediator.Object);
            mockMediator.Setup(x => x.Send(It.IsAny<SellStockCommand>(), default(CancellationToken))).ReturnsAsync(customerTrading);
            var response = await controller.SellStocks(It.IsAny<SellStockRequest>());
            Assert.NotNull(response);
        }

        [Fact]
        public async Task SendQuotes_ReturnSuccess()
        {
            var customerQuotes = new CustomerQuotes(1, "test", DateTime.Now, 1, 100, true);
            var mockMediator = new Mock<IMediator>();
            var controller = new CustomerTradingController(mockMediator.Object);
            mockMediator.Setup(x => x.Send(It.IsAny<SendQuotesCommand>(), default(CancellationToken))).ReturnsAsync(customerQuotes);
            var response = await controller.SendQuotes(It.IsAny<SendQuotesRequest>());
            Assert.NotNull(response);
        }

    }
}
