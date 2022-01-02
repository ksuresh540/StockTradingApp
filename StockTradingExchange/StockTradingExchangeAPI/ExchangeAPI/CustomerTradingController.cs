using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockTradingExchange.Application.Stocks.BuyStocks;
using StockTradingExchange.Application.Stocks.SellStocks;
using StockTradingExchange.Application.Stocks.SendQuotes;
using StockTradingExchange.Domain;
using System.Net;
using System.Threading.Tasks;

namespace StockTradingExchange.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTradingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerTradingController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("api/buystocks")]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerTrading), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> BuyStocks([FromBody] BuyStockRequest request)
        {
            var stock = await _mediator.Send(new BuyStockCommand(request.CustomerId, request.Name, request.Quantity, request.Price, request.StockType));
            return Created(string.Empty, stock);
        }

        [Route("api/sellstocks")]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerTrading), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> SellStocks([FromBody] SellStockRequest request)
        {
            var stock = await _mediator.Send(new SellStockCommand(request.CustomerId, request.Name, request.Quantity, request.Price, request.StockType));
            return Created(string.Empty, stock);
        }

        [Route("api/sendQuotes")]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerTrading), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> SendQuotes([FromBody] SendQuotesRequest request)
        {
            var stock = await _mediator.Send(new SendQuotesCommand(request.CustomerId, request.QuoteName, request.CreatedDate, request.Quantity, request.Price));
            return Created(string.Empty, stock);
        }
    }
}
