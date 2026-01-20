
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BitcoinApp.Server.Services
{
    public sealed class BitcoinWorker : BackgroundService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IExchangeRateService exchangeRateService;
        private readonly IRetrievedValuesService retrievedValuesService;
        private readonly ILogger<BitcoinWorker> logger;
        private readonly TimeSpan interval;
        private readonly string endpoint;
        
        public BitcoinWorker(IHttpClientFactory httpClientFactory, IExchangeRateService exchangeRateService, IRetrievedValuesService retrievedValuesService, ILogger<BitcoinWorker> logger, IConfiguration config)
        {
            this.httpClientFactory = httpClientFactory;
            this.exchangeRateService = exchangeRateService;
            this.retrievedValuesService = retrievedValuesService;
            this.logger = logger;

            var endpointConfig = config.GetValue<string>("BitcoinWorker:Endpoint") ?? throw new Exception("Configuration - BitcoinWorker:Endpoint not set");
            var seconds = config.GetValue<int?>("BitcoinWorker:IntervalSeconds") ?? throw new Exception("Configuration - BitcoinWorker:IntervalSeconds not set");
            
            interval = TimeSpan.FromSeconds(seconds);
            endpoint = endpointConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("BitcoinWorker starting. Endpoint: {Endpoint}, IntervalSeconds: {IntervalSeconds}", endpoint, interval.TotalSeconds);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var client = httpClientFactory.CreateClient("BitcoinWorkerClient");

                    var response = await client.GetAsync(endpoint, stoppingToken);
                    
                    response.EnsureSuccessStatusCode();
                    
                    var content = await response.Content.ReadAsStringAsync();

                    logger.LogInformation("GET {Endpoint} returned {StatusCode}. Payload length: {Length}", endpoint, response.StatusCode, content?.Length ?? 0);
                
                    if (content is null)
                    {
                        logger.LogWarning("GET {Endpoint} returned null content.", endpoint);

                        continue;
                    }

                    await ProcessApiResponse(content);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break; // graceful shutdown
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error calling GET {Endpoint}", endpoint);
                }

                try
                {
                    await Task.Delay(interval, stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }

            logger.LogInformation("BitcoinWorker stopping.");
        }


        private async Task ProcessApiResponse(string content)
        {
            var document = JsonDocument.Parse(content);
            var data = document.RootElement.GetProperty("Data");
            var valueEur = data.GetProperty("BTC-EUR").GetProperty("PRICE").GetDecimal();

            var retrievedAt = DateTime.UtcNow;
            var currentDate = DateOnly.FromDateTime(retrievedAt);

            if (retrievedValuesService.ExchangeRate.TryGetExchangeRateForDate(currentDate, out var exchangeRate))
            {
                exchangeRate = await exchangeRateService.GetExchangeRateAsync(currentDate);
                retrievedValuesService.ExchangeRate.SetExchangeRate(currentDate, exchangeRate);

                logger.LogInformation("Fetched exchange rate for {Date}: {ExchangeRate}", currentDate, exchangeRate);
            }
            
            decimal valueCzk = Math.Round(valueEur * exchangeRate, 2);

            var id = Guid.NewGuid();

            retrievedValuesService.AddRetrievedValue(id, retrievedAt, valueEur, valueCzk, exchangeRate);

            logger.LogInformation("Added Bitcoin value '{valueEur}' EUR => '{valueCzk}' CZK [rate '{exchangeRate}'] at {retrievedAt} - {id}", valueEur, valueCzk, exchangeRate, retrievedAt, id);
        }
    }
}