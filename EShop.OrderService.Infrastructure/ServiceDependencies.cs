using EShop.OrderService.Application.Repositories;
using EShop.OrderService.Infrastructure.ConfigSettings;
using EShop.OrderService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Infrastructure
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration config)
        {
            // Register your infrastructure dependencies here
            //services.AddDbContext<OrderDbContext>(options =>
            //    options.UseSqlServer("YourConnectionStringHere")); // Replace with your actual connection string
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentGateway, StripePaymentGateway>();

            services.Configure<StripeSettings>(config.GetSection("Stripe"));
            return services;
        }
    }
}
