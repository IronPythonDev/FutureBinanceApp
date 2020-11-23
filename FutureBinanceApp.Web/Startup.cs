using System.IO;
using FutureBinanceApp.Infrastructure.Data.Contexts;
using FutureBinanceApp.Web.Application.AsyncInitialization;
using FutureBinanceApp.Web.AsyncInitialization;
using FutureBinanceApp.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FutureBinanceApp.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            const string AssemblyName = "FutureBinanceApp.Infrastructure.Data";
            services.AddDbContext<AccountContext>(builder =>
            {
                if (_env.IsDevelopment())
                {
                    builder.EnableSensitiveDataLogging();
                }
                builder.UseSqlServer(connectionString, x => x.MigrationsAssembly(AssemblyName))
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleExceptionWithAggregateOperatorWarning));
            });
            services.AddDbContext<LogContext>(builder =>
            {
                builder.UseSqlServer(connectionString, x => x.MigrationsAssembly(AssemblyName))
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleExceptionWithAggregateOperatorWarning));
            });

            services.AddAsyncInitializer<MigrationsInitializer<AccountContext>>();
            services.AddAsyncInitializer<MigrationsInitializer<LogContext>>();
            services.AddAsyncInitializer<AccountInitializer>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , ILoggerFactory loggerFactory)
        {
            const string LOGS = "Logs";
            if (!Directory.Exists(LOGS))
            {
                Directory.CreateDirectory(LOGS);
            }

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), LOGS, "LogFile.log"));
            var logger = loggerFactory.CreateLogger("FileLogger");

            loggerFactory.AddContext(Configuration.GetConnectionString("DefaultConnection"));
            var dbLogger = loggerFactory.CreateLogger("DBLogger");
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
