using Microsoft.EntityFrameworkCore;
using System.Linq;
using StockTradingExchange.Domain;
using StockTradingExchange.Domain.StockTrading;
using StockTradingExchange.Infrastructure.Database;
using System;
using System.Threading.Tasks;

namespace StockTradingExchange.Infrastructure.Domain.StockTrading
{
    public class CustomerQuotesRepository : ICustomerQuotesRepository
    {
        private readonly CustomerQuotesContext _context;

        public CustomerQuotesRepository(CustomerQuotesContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(CustomerQuotes customer) => await this._context.CustomerQuotes.AddAsync(customer);

        public void Update(CustomerQuotes customer) => this._context.CustomerQuotes.Update(customer);

        public async Task<CustomerQuotes> GetByIdAsync(int id)
        {
            return await this._context.CustomerQuotes.AsNoTracking()
                .SingleAsync(x => x.CustomerId == id);
        }

        public async Task<CustomerQuotes> GetLastestQuoteByIdAsync(int customerId)
        {
            return await this._context.CustomerQuotes.AsNoTracking()
                .Where(x => x.CustomerId == customerId && x.IsActive == true)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();
        }

    }
}