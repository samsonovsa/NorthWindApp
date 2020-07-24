using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthWindApp.BLL.ConfigurationOptions;
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
            int counItemOnPage = configuration.GetSection(ProductOptions.Products).Get<ProductOptions>().MaxCountOnPage;

            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>(provider =>
              new UnitOfWork(provider.GetService<NorthwindContext>()));
            services.AddScoped<IDictionaryService, DictionaryService>(provider =>
               new DictionaryService(provider.GetService<IUnitOfWork>(),
                provider.GetService<ILogger<DictionaryService>>(),
                counItemOnPage));
            return services;
        }
    }
}
