using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SodimacGW.API.Config;
using System.Net.Http;

namespace SodimacGW.API.Services
{
    public class OrdenCompraService : IOrdenCompraService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrdenCompraService> _logger;
        private readonly UrlsConfig _urls;

        public OrdenCompraService(HttpClient httpClient, ILogger<OrdenCompraService> logger, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }
    }
}
