using System;
using StockTradingExchange.Application.Configuration.Commands;
using StockTradingExchange.Domain;

namespace StockTradingExchange.Application.Stocks.SendQuotes
{
    public class SendQuotesCommand : CommandBase<CustomerQuotes>
    {
        public int CustomerId { get; set; }

        public string QuoteName { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public SendQuotesCommand(int customerId, string name, DateTime createdDate, int quantity, double price)
        {
            this.CustomerId = customerId;
            this.QuoteName = name;
            this.Quantity = quantity;
            this.Price = price;
            this.CreatedDate = createdDate;
        }
    }
}