using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SodimacGW.API.Config;
using System.Net.Http;

namespace SodimacGW.API.Services
{
    public class ASNService : IASNService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ASNService> _logger;
        private readonly UrlsConfig _urls;

        public ASNService(HttpClient httpClient, ILogger<ASNService> logger, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }
    }
}
