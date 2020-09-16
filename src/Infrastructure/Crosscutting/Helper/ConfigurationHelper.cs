
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Helper
{
    public static class ConfigurationHelper
    {
        private const string JsonConfigurationProviderType = "Microsoft.Extensions.Configuration.Json.JsonConfigurationProvider";

        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json");
            
            //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true);

            IConfiguration configuration = builder.Build();
            
            return builder.Build();
        }

        public static string GetValue(this IConfigurationRoot configuration, string configurationKey)
        {
            string value = "";

            //First find in .jsp
            var jsonConfigurationProvider = configuration.Providers.Where(p => p.GetType().ToString() == JsonConfigurationProviderType).FirstOrDefault();
            if (jsonConfigurationProvider != null)
            {
                jsonConfigurationProvider.TryGet(configurationKey, out value);
            }

            if (string.IsNullOrEmpty(value))
            {
                foreach (var provider in configuration.Providers.Where(p => p.GetType().ToString() != JsonConfigurationProviderType))
                {
                    provider.TryGet(configurationKey, out value);
                }
            }

            return value;
        }
    }
}