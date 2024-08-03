using Microsoft.AspNetCore.Diagnostics;

using OneReview.Persistence.Database;

namespace OneReview.RequestPipeline;

public static class WebApplicationExtensions
{
    public static WebApplication InitializeDatabase(this WebApplication app)
    {
        DBInitializer.Initialize(app.Configuration[DBConstants.DefaultConnectionStringPath]!);

        return app;
    }

    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        app.Map("/error", (HttpContext httpContext) =>
        {
            Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception is not null)
            {
                return Results.Problem();
            }

            return Results.Problem();
        });

        return app;
    }
}
