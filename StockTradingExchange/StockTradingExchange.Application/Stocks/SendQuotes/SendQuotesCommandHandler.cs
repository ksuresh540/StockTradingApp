using StockTradingExchange.Application.Configuration.Commands;
using StockTradingExchange.Domain;
using StockTradingExchange.Domain.SeedWork;
using StockTradingExchange.Domain.StockTrading;
using System.Threading;
using System.Threading.Tasks;

namespace StockTradingExchange.Application.Stocks.SendQuotes
{
    public class SendQuotesCommandHandler : ICommandHandler<SendQuotesCommand, CustomerQuotes>
    {
        private readonly ICustomerQuotesRepository _customerQuotesRepository;
        private readonly ICustomerTradingRepository _stockTradingExchangeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SendQuotesCommandHandler(ICustomerQuotesRepository customerQuotesRepository, ICustomerTradingRepository stockTradingExchangeRepository,
            IUnitOfWork unitOfWork)
        {
            this._customerQuotesRepository = customerQuotesRepository;
            this._stockTradingExchangeRepository = stockTradingExchangeRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<CustomerQuotes> Handle(SendQuotesCommand request, CancellationToken cancellationToken)
        {

            var quotes = CustomerQuotes.OrderQuotes(request.CustomerId, request.QuoteName, request.CreatedDate, request.Quantity,
                request.Price, true);

            var customerQuotes = await this._customerQuotesRepository.GetLastestQuoteByIdAsync(request.CustomerId);
            customerQuotes.IsActive = false; //Making old quote inactive.
            this._customerQuotesRepository.Update(customerQuotes);

            //Deduct money
            var tradingDetails = await _stockTradingExchangeRepository.GetByIdAsync(request.CustomerId);
            var moneyleft = tradingDetails.TotalAmount - request.Price;
            tradingDetails.TotalAmount = moneyleft;

            

            await this._customerQuotesRepository.AddAsync(quotes);
            this._stockTradingExchangeRepository.Update(tradingDetails);
            await this._unitOfWork.CommitAsync(cancellationToken);
            return quotes;
        }
    }
}