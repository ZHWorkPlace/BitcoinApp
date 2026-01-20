
namespace BitcoinApp.Server.Models
{
    public class ExchangeRate
    {
        private DateOnly? Date { get; set; }
        private decimal Rate { get; set; }


        public void SetExchangeRate(DateOnly date, decimal rate)
        {
            Date = date;
            Rate = rate;
        }

        public bool TryGetExchangeRateForDate(DateOnly date, out decimal rate)
        {
            if (Date == date)
            {
                rate = Rate;
                return false;
            }

            rate = 0;
            return true;
        }
    }
}