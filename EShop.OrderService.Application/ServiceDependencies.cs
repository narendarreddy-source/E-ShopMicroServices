using EShop.OrderService.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EShop.OrderService.Application
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            // Register your application dependencies here
            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
            services.AddScoped<IOrderService, Services.Implementaions.OrderService>();

            return services;
        }
    }
}
