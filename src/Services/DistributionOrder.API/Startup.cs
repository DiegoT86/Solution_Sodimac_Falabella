using Agents;
using DistributionOrder.API.Config;
using DistributionOrder.API.Domain.Contracts;
using DistributionOrder.API.Domain.Services;
using DistributionOrder.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;

namespace DistributionOrder.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISodimacDBManager, SodimacDBManager>();
            services.AddHttpClient<IApiCaller, ApiCaller>();
            services.AddTransient<IDistributionOrderRepository, DistributionOrderRepository>();
            services.AddTransient<IDistributionOrderService, DistributionOrderService>();            

            services.AddControllers();

            SwaggerConfig.AddRegistration(services);
            services.Configure<ApiConfig>(Configuration.GetSection("ApiConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SwaggerConfig.AddRegistration(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
