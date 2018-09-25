using Library1C;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WebApiBusinessLogic;

namespace WebApiFPA
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfiguration config;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<ILogger>(provider => {
                return new LoggerConfiguration()
                    .WriteTo.Seq("http://logs.fitness-pro.ru:5341")
                    .CreateLogger();
            });

            services.AddScoped(mapper => { return new TypeAdapterConfig(); });

            services.AddScoped<BusinessLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
