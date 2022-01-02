namespace StockTradingExchange.Application.Stocks.SellStocks
{
    public class SellStockRequest
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string StockType { get; set; }

    }
}