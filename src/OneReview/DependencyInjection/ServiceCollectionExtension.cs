using OneReview.Services;

namespace OneReview.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ProductService>();
        services.AddScoped<ReviewService>();

        return services;
    }
}
