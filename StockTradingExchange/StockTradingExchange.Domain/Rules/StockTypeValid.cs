using System;
using StockTradingExchange.Domain.SeedWork;

namespace StockTradingExchange.Domain.Rules
{
    public class StockTypeValid : IBusinessRule
    {
        private readonly string _stockType;

        public StockTypeValid(string stockType)
        {
            _stockType = stockType;
        }

        public bool IsError()
        {
            var convertedValue = Enum.TryParse(_stockType, out StockTypeEnum stockTypeEnum);
            if (!convertedValue)
            {
                return true; 
            }
            return false;
        }

        public string Message => "Stock Type is invalid";
    }
}