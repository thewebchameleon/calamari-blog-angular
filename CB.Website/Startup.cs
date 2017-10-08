using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CB.Infrastructure.Cache;
using CB.Infrastructure.Cache.Configuration;
using CB.Infrastructure.Repositories;
using CB.Services.Blog;
using CB.Services.Blog.Mappers;
using CB.CMS.SquidexClient;
using Microsoft.AspNetCore.ResponseCompression;
using CB.Domain.Services.Profile.Mappers;
using CB.Domain.Services.Profile;
using CB.Repositories.Profile;

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

            //cache
            services.AddSingleton<ICacheConfiguration, CacheConfiguration>();
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();

            //cms client
            services.AddSingleton<ISquidexConfiguration, SquidexConfiguration>();
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
    }
}
