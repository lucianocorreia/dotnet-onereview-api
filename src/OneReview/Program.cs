using OneReview.DependencyInjection;
using OneReview.Persistence.Database;
using OneReview.RequestPipeline;
using OneReview.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddServices()
        .AddControllers();

}

var app = builder.Build();
{
    app.MapControllers();
    app.InitializeDatabase();
}

app.Run();
