using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceOptimizerCoreApplication.web.Installers;
using System.Threading.Tasks;

namespace PriceOptimizerCoreApplication.web
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
            services.InstallServicesInAssembly(Configuration);
            services.AddControllersWithViews();
            //services.AddAutoMapper(typeof(Startup));
            //services.AddScoped<IProductRepository, ProductRepository>(); 
            // services.AddScoped<ISpGenericRepository,SpGenericRepository>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseExceptionHandler("/Error");
               // app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseCors(builder =>
            //builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString())
            //.AllowAnyHeader()
            //.AllowAnyMethod()

            //);
            app.UseCors();
             app.Use(async (ctx, next) =>
            {
                await next();

                if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                {
                    //Re-execute the request so the user gets the error page
                    string originalPath = ctx.Request.Path.Value;
                    ctx.Items["originalPath"] = originalPath;
                    ctx.Request.Path = "/Error/404";
                    await next();
                }
                else if (ctx.Response.StatusCode == 500 && !ctx.Response.HasStarted)
                {
                    string originalPath = ctx.Request.Path.Value;
                    ctx.Items["originalPath"] = originalPath;
                    ctx.Request.Path = "/Error/500";
                    await next();
                }
            });

            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); 

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(
                //    name: "defaultBlog",
                //    pattern: "{controller=Blog}/{action=Index}/{id?}");
            });
            
            ////app.UseMvc();
            //app.UseMvc(routes =>
            //{

            //    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});

        }
    }
}
