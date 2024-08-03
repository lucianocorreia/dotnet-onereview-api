using OneReview.DependencyInjection;
using OneReview.Persistence.Database;
using OneReview.RequestPipeline;
using OneReview.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddGlobalErrorHandling()
        .AddServices()
        .AddPersistence(builder.Configuration)
        .AddControllers();

}

var app = builder.Build();
{
    app.UseGlobalErrorHandling();
    app.MapControllers();
    app.InitializeDatabase();
}

app.Run();
