using EShop.OrderService.Application.Repositories;
using EShop.OrderService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Infrastructure
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            // Register your infrastructure dependencies here
            //services.AddDbContext<OrderDbContext>(options =>
            //    options.UseSqlServer("YourConnectionStringHere")); // Replace with your actual connection string
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
