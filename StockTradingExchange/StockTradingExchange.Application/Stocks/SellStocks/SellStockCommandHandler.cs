using StockTradingExchange.Application.Configuration.Commands;
using StockTradingExchange.Domain;
using StockTradingExchange.Domain.SeedWork;
using StockTradingExchange.Domain.StockTrading;
using System.Threading;
using System.Threading.Tasks;

namespace StockTradingExchange.Application.Stocks.SellStocks
{
    public class SellStockCommandHandler : ICommandHandler<SellStockCommand, CustomerTrading>
    {
        private readonly ICustomerTradingRepository _stockTradingExchangeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SellStockCommandHandler(ICustomerTradingRepository stockRepository, IUnitOfWork unitOfWork)
        {
            this._stockTradingExchangeRepository = stockRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<CustomerTrading> Handle(SellStockCommand request, CancellationToken cancellationToken)
        {
            var customerTrading = await this._stockTradingExchangeRepository.GetByIdAsync(request.CustomerId);
            var buyStock = CustomerTrading.PurchaseStocks(request.CustomerId, request.Name, request.Quantity, request.Price,
                request.StockType, customerTrading.TotalAmount);

            await this._stockTradingExchangeRepository.AddAsync(buyStock);
            await this._unitOfWork.CommitAsync(cancellationToken);
            return buyStock;
        }
    }
}