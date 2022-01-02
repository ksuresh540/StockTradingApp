using System;
using StockTradingExchange.Domain.SeedWork;

namespace StockTradingExchange.Domain.Rules
{
    public class BalanceLimitValid : IBusinessRule
    {
        private readonly int _existingTotalAmount;
        private readonly int _newTotalAmount;

        public BalanceLimitValid(int existingTotalAmount, int newTotalAmount)
        {
            _existingTotalAmount = existingTotalAmount;
            _newTotalAmount = newTotalAmount;
        }

        public bool IsError()
        {
            if (_newTotalAmount > _existingTotalAmount)
            {
                return true; 
            }
            return false;
        }

        public string Message => "Purachase stocks exceeds balance in the account";
    }
}