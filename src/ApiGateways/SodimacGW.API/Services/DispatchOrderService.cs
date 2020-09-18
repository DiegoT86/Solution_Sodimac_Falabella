using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SodimacGW.API.Config;
using System.Net.Http;

namespace SodimacGW.API.Services
{
    public class DispatchOrderService : IDispatchOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DispatchOrderService> _logger;
        private readonly UrlsConfig _urls;

        public DispatchOrderService(HttpClient httpClient, ILogger<DispatchOrderService> logger, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }
    }
}
