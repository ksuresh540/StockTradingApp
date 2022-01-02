using Microsoft.EntityFrameworkCore;
using StockTradingExchange.Domain;

namespace StockTradingExchange.Infrastructure.Database
{
    public class CustomerQuotesContext : DbContext
    {
        public DbSet<CustomerQuotes> CustomerQuotes { get; set; }

        public CustomerQuotesContext(DbContextOptions<CustomerQuotesContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerQuotesContext).Assembly);
        }
    }
}
