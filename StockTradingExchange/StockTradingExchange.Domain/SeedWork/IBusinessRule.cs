namespace StockTradingExchange.Domain.SeedWork
{
    public interface IBusinessRule
    {
        bool IsError();

        string Message { get; }
    }
}