using Common.Interfaces;
using Common.Logging;
using Library1C;
using LibraryAmoCRM;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<ILoggerService, LoggerService>();

            services.AddScoped(mapper => { return new TypeAdapterConfig(); });

            services.AddScoped(amo => {
                return new DataManager(
                    Configuration.GetSection("providers:0:AmoCRM:connection:account:name").Value,
                    Configuration.GetSection("providers:0:AmoCRM:connection:account:email").Value,
                    Configuration.GetSection("providers:0:AmoCRM:connection:account:hash").Value
                );
            });

            services.AddScoped(service1C => {
                return new UnitOfWork(
                    Configuration.GetSection("providers:1:1C:connection:account:user").Value,
                    Configuration.GetSection("providers:1:1C:connection:account:pass").Value
                );
            });

            services.AddSingleton( neo =>
            {
                return new ServiceLibraryNeoClient.Implements.DataManager(
                    new System.Uri( Configuration.GetSection( "providers:2:Neo:connection:account:uri" ).Value ),
                        Configuration.GetSection( "providers:2:Neo:connection:account:user" ).Value,
                        Configuration.GetSection( "providers:2:Neo:connection:account:pass" ).Value
                    );
            } );

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
