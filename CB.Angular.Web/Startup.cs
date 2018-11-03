using CB.Angular.CMS;
using CB.Angular.CMS.Contracts;
using CB.Angular.CMS.Mappers;
using CB.Angular.CMS.Mappers.Contracts;
using CB.Angular.Infrastructure.Cache;
using CB.Angular.Infrastructure.Configuration;
using CB.Angular.Infrastructure.Repositories.SquidexRepo;
using CB.Angular.Infrastructure.Repositories.SquidexRepo.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;

namespace CB.Angular.Web
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
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //compress static files with gzip
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
                    // Default
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    // Custom
                    "image/svg+xml",
                };
            });

            //config
            services.AddSingleton(Configuration);
            services.Configure<CacheConfig>(options => Configuration.GetSection("Cache").Bind(options));
            services.Configure<LoggingConfig>(options => Configuration.GetSection("Logging").Bind(options));
            services.Configure<SquidexConfig>(options => Configuration.GetSection("Squidex").Bind(options));

            //cache
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();

            //mappers
            services.AddTransient<ICMSMapper, CMSMapper>();

            //repos
            services.AddTransient<ISquidexRepo, SquidexRepo>();

            //services
            services.AddTransient<ICMSService, CMSService>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<LoggingConfig> loggerConfig)
        {
            ConfigureLogging(loggerFactory, loggerConfig);
            ConfigureExeptionHandling(app, env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }

        public void ConfigureLogging(ILoggerFactory loggerFactory, IOptions<LoggingConfig> settings)
        {
            var config = settings.Value;

            var loggingLevel = new LoggingLevelSwitch(Enum.Parse<LogEventLevel>(config.MinimumLogLevel));
            var logger = new LoggerConfiguration().MinimumLevel.ControlledBy(loggingLevel);

            if (config.Seq.IsEnabled)
            {
                logger.WriteTo.Seq(
                    config.Seq.Endpoint,
                    controlLevelSwitch: loggingLevel
                );
            }
            if (config.RollingFile.IsEnabled)
            {
                //bump up the minimum logging level otherwise the log files will be huge
                logger.WriteTo.RollingFile(config.RollingFile.Path, restrictedToMinimumLevel: LogEventLevel.Warning);
            }

            loggerFactory.AddSerilog(logger.CreateLogger());
        }

        public void ConfigureExeptionHandling(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
        }
    }
}
