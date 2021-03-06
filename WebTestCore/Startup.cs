using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebTestCore.Filters;
using WebInterface.EfServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace WebTestCore
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
            services.AddControllersWithViews();

            //services.AddTransient<ISecurityService, EfSecurityServices>();
            //services.AddTransient<ISecurityService, AdoSecurityService>();
            services.AddTransient<ISecurityService, WebApiSecurityServices>();


            //services.AddMvcCore(options =>
            //    options.Filters.Add(new HeaderActionFilter()));

            //services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = false;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
