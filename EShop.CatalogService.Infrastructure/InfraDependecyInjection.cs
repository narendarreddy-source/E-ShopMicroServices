using EShop.CatalogService.Application.Repositories;
using EShop.CatalogService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Infrastructure
{
    public static class InfraDependecyInjection
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection services)
        {

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProdcutRepository, ProductRepository>();

            return services;
        }
    }
}
