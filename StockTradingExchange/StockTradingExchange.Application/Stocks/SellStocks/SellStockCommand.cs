using StockTradingExchange.Application.Configuration.Commands;
using StockTradingExchange.Domain;

namespace StockTradingExchange.Application.Stocks.SellStocks
{
    public class SellStockCommand : CommandBase<CustomerTrading>
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string StockType { get; set; }

        public SellStockCommand(int customerId, string name, int quantity, double price, string stockType)
        {
            this.CustomerId = customerId;
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
            this.StockType = stockType;
        }
    }
}