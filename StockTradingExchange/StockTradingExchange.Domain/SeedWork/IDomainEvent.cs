using System;
using MediatR;

namespace StockTradingExchange.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}