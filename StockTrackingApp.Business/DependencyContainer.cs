using Microsoft.Extensions.DependencyInjection;
using StockTrackingApp.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IColorService, ColorService>();  


            return services;
        }
    }
}
