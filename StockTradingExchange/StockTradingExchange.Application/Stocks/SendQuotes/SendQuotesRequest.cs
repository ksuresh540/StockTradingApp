using System;

namespace StockTradingExchange.Application.Stocks.SendQuotes
{
    public class SendQuotesRequest
    {
        public int CustomerId { get; set; }

        public string QuoteName { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

    }
}