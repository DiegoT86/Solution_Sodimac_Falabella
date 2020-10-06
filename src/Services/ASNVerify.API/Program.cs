using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ASNVerify.API
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            CreateHostBuilder(configuration, args).Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        private static IWebHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .CaptureStartupErrors(false)
                .ConfigureKestrel(options =>
                {
                    var ports = GetDefinedPorts(configuration);
                    options.Listen(IPAddress.Any, ports, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    });
                })
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build();

        //private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        //{
        //    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
        //    var logstashUrl = configuration["Serilog:LogstashgUrl"];
        //    return new LoggerConfiguration()
        //        .MinimumLevel.Verbose()
        //        .Enrich.WithProperty("ApplicationContext", AppName)
        //        .Enrich.FromLogContext()
        //        .WriteTo.Console()
        //        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
        //        .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
        //        .ReadFrom.Configuration(configuration)
        //        .CreateLogger();
        //}

        private static int GetDefinedPorts(IConfiguration config)
        {
            var port = config.GetValue("PORT", 80);
            return port;
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //var config = builder.Build();

            //if (config.GetValue<bool>("UseVault", false))
            //{
            //    builder.AddAzureKeyVault(
            //        $"https://{config["Vault:Name"]}.vault.azure.net/",
            //        config["Vault:ClientId"],
            //        config["Vault:ClientSecret"]);
            //}

            return builder.Build();
        }
    }
}
