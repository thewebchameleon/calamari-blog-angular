using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WC.Blog.Infrastructure.Cache;
using WC.Blog.Infrastructure.Cache.Configuration;
using WC.Blog.Infrastructure.Repositories;
using WC.Blog.Services.Blog;
using WC.Blog.Services.Blog.Mappers;
using WC.CMS.SquidexClient;

namespace WebChameleon_Blog
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

            services.AddSingleton(Configuration);

            //cache
            services.AddSingleton<ICacheConfiguration, CacheConfiguration>();
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();

            //cms client
            services.AddSingleton<ISquidexConfiguration, SquidexConfiguration>();
            services.AddTransient<ISquidexClientFactory, SquidexClientFactory>();

            //repositories
            services.AddTransient<IBlogRepository, BlogRepository>();

            //services
            services.AddTransient<IBlogMapper, BlogMapper>();
            services.AddTransient<IBlogService, BlogService>();
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
