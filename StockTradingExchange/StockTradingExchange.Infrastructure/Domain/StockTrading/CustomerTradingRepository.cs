using Microsoft.EntityFrameworkCore;
using StockTradingExchange.Domain;
using StockTradingExchange.Domain.StockTrading;
using StockTradingExchange.Infrastructure.Database;
using System;
using System.Threading.Tasks;

namespace StockTradingExchange.Infrastructure.Domain.StockTrading
{
    public class CustomerTradingRepository : ICustomerTradingRepository
    {
        private readonly CustomerTradingContext _context;

        public CustomerTradingRepository(CustomerTradingContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(CustomerTrading customer) => await this._context.CustomerTrading.AddAsync(customer);

        public void Update(CustomerTrading customer) => this._context.CustomerTrading.Update(customer);

        public async Task<CustomerTrading> GetByIdAsync(int id)
        {
            return await this._context.CustomerTrading.AsNoTracking()
                .SingleAsync(x => x.CustomerId == id);
        }



    }
}