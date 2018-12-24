using Common.Interfaces;
using Common.Logging;
using Library1C;
using LibraryAmoCRM;
using LibraryAmoCRM.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Serilog;
using Serilog.Extensions.Logging;
using System.Net.Http;
using WebApiBusinessLogic;

namespace WebApiFPA
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("~Application", configuration["applicationName"])
                .Enrich.WithProperty("~Enviroment", configuration["ENVIRONMENT"])
                .WriteTo.LiterateConsole()
                .WriteTo.Seq("http://logs.fitness-pro.ru:5341")
                .CreateLogger();
        }
        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .SetMinimumLevel(LogLevel.Trace)
                //.AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Information)
                //.AddFilter<ConsoleLoggerProvider>("System", LogLevel.Information)
                //.AddFilter<SerilogLoggerProvider>("Microsoft", LogLevel.Warning)
                //.AddFilter<SerilogLoggerProvider>("System", LogLevel.Warning)
                //.AddConsole()
                .AddSerilog()
                ); 

            services.AddMvc();

            services.AddCors();

            services.AddMemoryCache();

            services.AddScoped<ILoggerService, LoggerService>();

            services.AddSingleton<Connection>( con =>
            {
                return new Connection(
                    Configuration.GetSection( "providers:0:AmoCRM:connection:account:name" ).Value,
                    Configuration.GetSection( "providers:0:AmoCRM:connection:account:email" ).Value,
                    Configuration.GetSection( "providers:0:AmoCRM:connection:account:hash" ).Value
                );
            } );

            services.AddScoped( mapper => { return new TypeAdapterConfig(); } );

            services.AddScoped<IDataManager, CrmManager>();

            services.AddScoped( service1C =>
            {
                return new UnitOfWork(
                    Configuration.GetSection( "providers:1:1C:connection:account:user" ).Value,
                    Configuration.GetSection( "providers:1:1C:connection:account:pass" ).Value
                );
            } );

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Connection connection)
        {
            connection.Auth(null);

            app.UseCors( builder => builder.AllowAnyOrigin()
                                         .AllowAnyHeader()
                                         .AllowAnyMethod() );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc( routes =>
             {
                 routes.MapRoute( "default", "{controller=Home}/{action=Index}/{id?}" );
             } );
        }
    }
}
