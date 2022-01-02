using StockTradingExchange.Application.Stocks.BuyStocks;
using System.Reflection;


namespace StockTradingExchange.Infrastructure
{
    public static class Assemblies
    {
        public static readonly Assembly Application = typeof(BuyStockCommand).Assembly;
    }
}