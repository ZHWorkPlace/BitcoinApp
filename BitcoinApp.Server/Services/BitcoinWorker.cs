
using System.Text.Json;

namespace BitcoinApp.Server.Services
{
    public sealed class BitcoinWorker : BackgroundService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IRetrievedValuesService retrievedValuesService;
        private readonly ILogger<BitcoinWorker> logger;
        private readonly TimeSpan interval;
        private readonly string endpoint;
        
        public BitcoinWorker(IHttpClientFactory httpClientFactory, IRetrievedValuesService retrievedValuesService, ILogger<BitcoinWorker> logger, IConfiguration config)
        {
            this.httpClientFactory = httpClientFactory;
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

                    ProcessApiResponse(content);
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

        private void ProcessApiResponse(string content)
        {
            var document = JsonDocument.Parse(content);
            var data = document.RootElement.GetProperty("Data");
            var bitcoinValue = data.GetProperty("BTC-EUR").GetProperty("PRICE").GetDecimal();
            var retrievedAt = DateTime.UtcNow;

            retrievedValuesService.AddRetrievedValue(retrievedAt, bitcoinValue);

            logger.LogInformation("Added Bitcoin value '{bitcoinValue}' at {retrievedAt}", bitcoinValue, retrievedAt);
        }
    }
}