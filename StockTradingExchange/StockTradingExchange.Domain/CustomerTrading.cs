using StockTradingExchange.Domain.Rules;
using StockTradingExchange.Domain.SeedWork;
using System;

namespace StockTradingExchange.Domain
{
    public class CustomerTrading : Entity, IAggregateRoot
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public StockTypeEnum StockType { get; set; }

        public double TotalAmount { get; set; }

        public CustomerTrading(int customerId, string name, int quantity, double price, string stockType, double totalAmount)
        {
            CheckRule(new StockTypeValid(stockType));
            CheckRule(new AllowedWinodw());
            CheckRule(new AllowedQueueWinodw());

            this.CustomerId = customerId;
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
            this.TotalAmount = totalAmount;
            Enum.TryParse(stockType, out StockTypeEnum stockTypeEnum);
            this.StockType = stockTypeEnum;
        }

        public static CustomerTrading PurchaseStocks(int customerId, string name, int quantity, double price, string stockType, double totalAmount)
        {
            return new CustomerTrading(customerId, name, quantity, price, stockType, totalAmount);
        }
    }

    public enum StockTypeEnum
    {
        Market,
        Limit
    }
}