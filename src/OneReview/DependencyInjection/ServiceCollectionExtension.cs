using Npgsql;

using OneReview.Persistence.Database;
using OneReview.Persistence.Database.Repositories;
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

    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(configuration[DBConstants.DefaultConnectionStringPath]!));
        services.AddScoped<ProductsRepository>();

        return services;
    }
}
