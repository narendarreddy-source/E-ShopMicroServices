
using EShop.CartService.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EShop.CartService.Application
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            // Register application services here
            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
            services.AddScoped<ICartService, Services.Implementation.CartService>();
            //services.AddScoped<ICartService, CartService>();
            //services.AddScoped<ICartService,CartService>();

            return services;
        }
    }
}
