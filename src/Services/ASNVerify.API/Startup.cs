using Agents;
using ASNVerify.API.Config;
using ASNVerify.API.Domain.Contracts;
using ASNVerify.API.Domain.Services;
using ASNVerify.API.Infrastucture.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;

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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISodimacDBManager, SodimacDBManager>();
            services.AddHttpClient<IApiCaller, ApiCaller>();
            services.AddTransient<IASNVerifyRepository, ASNVerifyRepository>();
            services.AddTransient<IASNVerifyService, ASNVerifyService>();

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
