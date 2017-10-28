using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CB.Infrastructure.Cache;
using CB.Infrastructure.Repositories;
using CB.Services.Blog;
using CB.Services.Blog.Mappers;
using CB.CMS.SquidexClient;
using Microsoft.AspNetCore.ResponseCompression;
using CB.Domain.Services.Profile.Mappers;
using CB.Domain.Services.Profile;
using CB.Repositories.Profile;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using Serilog.Events;
using Microsoft.Extensions.Options;
using CB.Common.Logging;
using Microsoft.ApplicationInsights.Extensibility;

namespace CB_Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly IConfiguration Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc();

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

            services.AddSingleton(Configuration);
            services.Configure<LoggingSettings>(options => Configuration.GetSection("Logging").Bind(options));

            //application insights
            services.AddApplicationInsightsTelemetry();

            //cache
            services.Configure<CacheSettings>(options => Configuration.GetSection("Cache").Bind(options));
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();

            //cms client
            services.Configure<SquidexSettings>(options => Configuration.GetSection("Squidex").Bind(options));
            services.AddTransient<ISquidexClientFactory, SquidexClientFactory>();

            //repositories
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IProfileRepository, ProfileRepository>();

            //mappers
            services.AddTransient<IBlogMapper, BlogMapper>();
            services.AddTransient<IProfileMapper, ProfileMapper>();

            //services
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IProfileService, ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<LoggingSettings> loggerConfig)
        {
            ConfigureLogging(loggerFactory, loggerConfig);
            ConfigureExeptionHandling(app, env);

            if (env.IsDevelopment())
            {
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }

            app.UseResponseCompression();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        public void ConfigureLogging(ILoggerFactory loggerFactory, IOptions<LoggingSettings> settings)
        {
            var config = settings.Value;

            var loggingLevel = new LoggingLevelSwitch(Enum.Parse<LogEventLevel>(config.MinimumLogLevel));
            var logger = new LoggerConfiguration().MinimumLevel.ControlledBy(loggingLevel);

            if (config.Seq.IsEnabled)
            {
                logger.WriteTo.Seq(
                    config.Seq.Endpoint,
                    apiKey: config.Seq.APIKey,
                    controlLevelSwitch: loggingLevel
                );
            }
            if (config.ApplicationInsights.IsEnabled)
            {
                logger.WriteTo.ApplicationInsightsEvents(new TelemetryConfiguration()
                {
                    InstrumentationKey = config.ApplicationInsights.InstrumentationKey,
                }, loggingLevel.MinimumLevel);
            }
            if (config.RollingFile.IsEnabled)
            {
                //bump up the minimum logging level otherwise the log files will be huge
                logger.WriteTo.RollingFile(config.RollingFile.Path, restrictedToMinimumLevel: LogEventLevel.Warning);
            }
            if (config.Console.IsEnabled)
            {
                loggerFactory.AddConsole();
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
                app.UseExceptionHandler("/Home/Error");
            }
        }
    }
}
