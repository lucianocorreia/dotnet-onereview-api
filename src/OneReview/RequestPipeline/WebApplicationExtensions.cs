using OneReview.Persistence.Database;

namespace OneReview.RequestPipeline;

public static class WebApplicationExtensions
{
    public static WebApplication InitializeDatabase(this WebApplication app)
    {
        DBInitializer.Initialize(app.Configuration[DBConstants.DefaultConnectionStringPath]!);

        return app;
    }
}
