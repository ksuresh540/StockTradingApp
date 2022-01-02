using Microsoft.EntityFrameworkCore;
using StockTradingExchange.Domain;

namespace StockTradingExchange.Infrastructure.Database
{
    public class CustomerTradingContext : DbContext
    {
        public DbSet<CustomerTrading> CustomerTrading { get; set; }

        public CustomerTradingContext(DbContextOptions<CustomerTradingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerTradingContext).Assembly);
        }
    }
}
