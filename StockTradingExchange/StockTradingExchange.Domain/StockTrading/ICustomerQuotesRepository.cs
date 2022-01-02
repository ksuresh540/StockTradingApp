using System;
using System.Threading.Tasks;

namespace StockTradingExchange.Domain.StockTrading
{
    public interface ICustomerQuotesRepository
    {
        Task<CustomerQuotes> GetByIdAsync(int id);

        Task AddAsync(CustomerQuotes customer);

        Task<CustomerQuotes> GetLastestQuoteByIdAsync(int customerId);

        void Update(CustomerQuotes customer);
    }
}