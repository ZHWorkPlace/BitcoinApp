using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace BitcoinApp.Server.Services
{
    public sealed class BitcoinWorker : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BitcoinWorker> _logger;
        private readonly IConfiguration _config;
        private readonly string _endpoint;
        private readonly TimeSpan _interval;

        public BitcoinWorker(IHttpClientFactory httpClientFactory, ILogger<BitcoinWorker> logger, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;

            // Configurable endpoint and interval with sensible defaults
            _endpoint = _config.GetValue<string>("BitcoinWorker:Endpoint") ?? "https://localhost:5001/weatherforecast";
            var seconds = _config.GetValue<int?>("BitcoinWorker:IntervalSeconds") ?? 60;
            _interval = TimeSpan.FromSeconds(Math.Max(1, seconds));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BitcoinWorker starting. Endpoint: {Endpoint}, IntervalSeconds: {IntervalSeconds}", _endpoint, _interval.TotalSeconds);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var client = _httpClientFactory.CreateClient("BitcoinWorkerClient");
                    var response = await client.GetAsync(_endpoint, stoppingToken);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("GET {Endpoint} returned {StatusCode}. Payload length: {Length}", _endpoint, response.StatusCode, content?.Length ?? 0);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    // graceful shutdown
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GET {Endpoint}", _endpoint);
                }

                try
                {
                    await Task.Delay(_interval, stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }

            _logger.LogInformation("BitcoinWorker stopping.");
        }
    }
}