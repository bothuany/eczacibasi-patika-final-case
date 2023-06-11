using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTrackingApp.Data.Interface;

namespace StockTrackingApp.Data
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StockTrackingContext>(option => option.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();

            return services;
        }
    }
}
