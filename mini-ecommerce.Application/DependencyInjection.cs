using Microsoft.Extensions.DependencyInjection;
using mini_ecommerce.Application.Services;
// using mini_ecommerce.Application.Interfaces.Services;

namespace mini_ecommerce.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ProductService>();
        services.AddScoped<OrderService>();

        return services;
    }
}