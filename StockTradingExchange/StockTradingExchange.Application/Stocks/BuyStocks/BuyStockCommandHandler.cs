using StockTradingExchange.Application.Configuration.Commands;
using StockTradingExchange.Domain;
using StockTradingExchange.Domain.SeedWork;
using StockTradingExchange.Domain.StockTrading;
using System.Threading;
using System.Threading.Tasks;

namespace StockTradingExchange.Application.Stocks.BuyStocks
{
    public class BuyStockCommandHandler : ICommandHandler<BuyStockCommand, CustomerTrading>
    {
        private readonly ICustomerTradingRepository _stockTradingExchangeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BuyStockCommandHandler(ICustomerTradingRepository stockRepository, IUnitOfWork unitOfWork)
        {
            this._stockTradingExchangeRepository = stockRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<CustomerTrading> Handle(BuyStockCommand request, CancellationToken cancellationToken)
        {
            var customerTrading = await this._stockTradingExchangeRepository.GetByIdAsync(request.CustomerId);
            var buyStock = CustomerTrading.PurchaseStocks(request.CustomerId, request.Name, request.Quantity, request.Price, request.StockType, customerTrading.TotalAmount);
            await this._stockTradingExchangeRepository.AddAsync(buyStock);
            await this._unitOfWork.CommitAsync(cancellationToken);
            return buyStock;
        }
    }
}