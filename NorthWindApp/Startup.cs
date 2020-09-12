using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NorthWindApp.BLL.Infrastructure;
using NorthWindApp.Helpers;
using NorthWindApp.Configuration;
using NorthWindApp.Filters;
using NorthWindApp.Middleware;

namespace NorthWindApp
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
            services.Configure<FilterOptions>(Configuration.GetSection(FilterOptions.Filters));

            services.AddBusinessLogicLayer(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddSwaggerDocument();

            services.AddControllersWithViews(options => { options.Filters.Add(typeof(LoggingActionFilter)); });

            //services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            logger.LogInformation($"Applicaton started {DateTime.Now} in location {Directory.GetCurrentDirectory()}");

            app.EnableRequestBodyBuffering();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseCacheImageMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "images",
                    pattern: "images/{id:int}",
                    new { controller = "Category", action = "GetImage" }
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
