using StockTradingExchange.Domain.Rules;
using StockTradingExchange.Domain.SeedWork;
using System;

namespace StockTradingExchange.Domain
{
    public class CustomerQuotes : Entity, IAggregateRoot
    {
        public int CustomerId { get; set; }

        public string QuoteName { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public bool IsActive { get; set; }

        public CustomerQuotes(int customerId, string quoteName, DateTime createdDate,int quantity, double price, bool isActive)
        {
            this.CustomerId = customerId;
            this.QuoteName = quoteName;
            this.Quantity = quantity;
            this.Price = price;
            this.CreatedDate = createdDate;
            this.IsActive = isActive;
        }

        public static CustomerQuotes OrderQuotes(int customerId, string quoteName, DateTime createdDate, int quantity, double price, bool isActive)
        {
            CheckRule(new AllowedWinodw());
            return new CustomerQuotes(customerId, quoteName, createdDate, quantity, price, isActive);
        }
    }
}