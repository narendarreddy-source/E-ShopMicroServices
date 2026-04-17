using EShop.CartService.Application.Repositories;
using EShop.CartService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Infrastructure
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection services)
        {

            services.AddScoped<ICartRepository, CartRepository>();
            return services;
        }

    }
}
