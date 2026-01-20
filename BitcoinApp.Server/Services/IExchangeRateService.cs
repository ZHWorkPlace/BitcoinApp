
namespace BitcoinApp.Server.Services
{
    public interface IExchangeRateService
    {
        Task<decimal> GetExchangeRateAsync(DateOnly date);
    }
}