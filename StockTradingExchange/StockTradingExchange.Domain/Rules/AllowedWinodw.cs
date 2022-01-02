using System;
using StockTradingExchange.Domain.SeedWork;

namespace StockTradingExchange.Domain.Rules
{
    public class AllowedWinodw : IBusinessRule
    {

        public bool IsError()
        {
            var time = DateTime.Now;
            var startTime = new TimeSpan(9, 0, 0);
            var endTime = new TimeSpan(15, 0, 0);
            //[Valid time frame]
            if( time.TimeOfDay >= startTime &&
                   time.TimeOfDay <= endTime)
            {
                return false;
            }

            return true;
        }

        public string Message => "Alowed window is 9:00 - 15:00";
    }
}