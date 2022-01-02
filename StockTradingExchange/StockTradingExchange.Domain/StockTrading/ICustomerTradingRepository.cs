using System;
using System.Threading.Tasks;

namespace StockTradingExchange.Domain.StockTrading
{
    public interface ICustomerTradingRepository
    {
        Task<CustomerTrading> GetByIdAsync(int id);

        Task AddAsync(CustomerTrading customer);

        void Update(CustomerTrading customer);
    }
}