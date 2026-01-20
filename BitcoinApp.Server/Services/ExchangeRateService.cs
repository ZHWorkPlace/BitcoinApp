using System.Globalization;
using System.Text.RegularExpressions;

namespace BitcoinApp.Server.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient httpClient;

        public ExchangeRateService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<decimal> GetExchangeRateAsync(DateOnly date)
        {
            var fromDate = date.AddDays(-7).ToString("dd.MM.yyyy");
            var toDate = date.ToString("dd.MM.yyyy");

            var queryString = $"vybrane.txt?od={fromDate}&do={toDate}&mena=EUR&format=txt";
            
            var response = await httpClient.GetAsync(queryString);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            // Get the latest available rate
            var exChangeRate = ParseContent(content).OrderByDescending(r => r.Date).FirstOrDefault();
            if (exChangeRate != default)
            {
                return exChangeRate.Rate;
            }

            throw new Exception("Exchange rate not found.");
        }


        private IEnumerable<(DateOnly Date, decimal Rate)> ParseContent(string content)
        {
            var pattern = @"\s*(?<date>\d{2}\.\d{2}\.\d{4})\|(?<rate>\d{1,3},\d+)\s*";
            var matches = Regex.Matches(content, pattern, RegexOptions.Singleline);
            var culture = new CultureInfo("cs-CZ"); // comma decimal separator

            foreach (Match m in matches)
            {
                var date = DateOnly.ParseExact(m.Groups["date"].Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                var rate = decimal.Parse(m.Groups["rate"].Value, culture);

                yield return (date, rate);
            }
        }
    }
}