using System;
using StockTradingExchange.Domain.SeedWork;

namespace StockTradingExchange.Domain.Rules
{
    public class AllowedQueueWinodw : IBusinessRule
    {

        public bool IsError()
        {
            var time = DateTime.Now;
            var startTime = new TimeSpan(8, 30, 0);
            var endTime = new TimeSpan(9, 0, 0);

            //[Valid time frame]
            if( time.TimeOfDay >= startTime &&
                   time.TimeOfDay <= endTime)
            {
                return false;
            }

            return true;
        }

        public string Message => "Alowed queue window is 8:30 - 9:00";
    }
}