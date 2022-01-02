namespace StockTradingExchange.Application.Stocks.BuyStocks
{
    public class BuyStockRequest
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string StockType { get; set; }

    }
}