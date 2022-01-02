using StockTradingExchange.Domain.SeedWork;
using StockTradingExchange.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace StockTradingExchange.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerTradingContext _customerTradingContext;

        public UnitOfWork(CustomerTradingContext customerTradingContext)
        {
            this._customerTradingContext = customerTradingContext;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this._customerTradingContext.SaveChangesAsync(cancellationToken);
        }
    }
}