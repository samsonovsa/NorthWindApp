using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthWindApp.BLL.Infrastructure.ConfigurationOptions;
using NorthWindApp.BLL.Interfaces;
using NorthWindApp.BLL.Services;
using NorthWindApp.DAL.EF;
using NorthWindApp.DAL.Interfaces;
using NorthWindApp.DAL.Repositories;

namespace NorthWindApp.BLL.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.Configure<ProductOptions>(configuration.GetSection(ProductOptions.Products));
            services.Configure<CacheOptions>(configuration.GetSection(CacheOptions.CacheImage));

            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connectionString));

            services.AddDefaultIdentity<IdentityUser>(options => 
                    options.SignIn.RequireConfirmedAccount = false
                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<NorthwindContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDictionaryService, DictionaryService>();
            services.AddSingleton<IGenericCacheService<byte[]>, CacheImageService>();
            services.AddSingleton<IEmailService, EmailService>();

            return services;
        }
    }
}
