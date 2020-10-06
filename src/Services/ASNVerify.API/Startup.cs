using Agents;
using ASNVerify.API.Config;
using ASNVerify.API.Domain.Contracts;
using ASNVerify.API.Domain.Services;
using ASNVerify.API.Infrastucture.Repositories;
using ASNVerify.API.IntegrationEvents.Contracts;
using ASNVerify.API.IntegrationEvents.Events;
using ASNVerify.API.IntegrationEvents.Handlers;
using ASNVerify.API.IntegrationEvents.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBusLib;
using EventBusLib.Abstractions;
using EventBusServiceBus;
using HealthChecks.UI.Client;
using IntegrationEventLogEF;
using IntegrationEventLogEF.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;
using System;
using System.Data.Common;
using System.Reflection;

namespace ASNVerify.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISodimacDBManager, SodimacDBManager>();
            services.AddSingleton<IApiCaller, ApiCaller>();
            services.AddTransient<IASNVerifyRepository, ASNVerifyRepository>();
            services.AddTransient<IASNVerifyService, ASNVerifyService>();

            services.AddCustomMVC(Configuration)
                .AddCustomDbContext(Configuration)
                .AddCustomOptions(Configuration)
                .AddIntegrationServices(Configuration)
                .AddEventBus(Configuration)
                .AddCustomHealthCheck(Configuration);

            SwaggerConfig.AddRegistration(services);
            services.Configure<ApiConfig>(Configuration.GetSection("ApiConfig"));

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //Configure logs

            //loggerFactory.AddAzureWebAppDiagnostics();
            //loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

            var pathBase = Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            SwaggerConfig.AddRegistration(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });

            ConfigureEventBus(app);
        }

        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<ASNVerifyReceivedIntegrationEvent, ASNVerifyReceivedIntegrationEventHandler>();
        }
    }


    public static class CustomExtensionMethods
    {
        //public static IServiceCollection AddAppInsight(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddApplicationInsightsTelemetry(configuration);
        //    services.AddApplicationInsightsKubernetesEnricher();

        //    return services;
        //}

        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            //services.AddControllers(options =>
            //{
            //    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            //}).AddNewtonsoftJson();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder
                .AddCheck("self", () => HealthCheckResult.Healthy());

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                hcBuilder
                    .AddAzureServiceBusTopic(
                        configuration["EventBusConnection"],
                        topicName: "asnverify_event_bus",
                        name: "asnverify-servicebus-check",
                        tags: new string[] { "servicebus" });
            }

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
            });

            return services;
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiConfig>(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IASNVerifyIntegrationEventService, ASNVerifyIntegrationEventService>();

            services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<ApiConfig>>().Value;
                var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

                var serviceBusConnection = new ServiceBusConnectionStringBuilder(settings.EventBusConnection);

                return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
            });

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusServiceBus.EventBusServiceBus>(sp =>
            {
                var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusServiceBus.EventBusServiceBus>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusServiceBus.EventBusServiceBus(serviceBusPersisterConnection, logger,
                    eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<ASNVerifyReceivedIntegrationEventHandler>();

            return services;
        }
    }

}
