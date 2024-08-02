using OneReview.Persistence.Database;
using OneReview.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<ProductService>();
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.MapControllers();

    Console.WriteLine(app.Configuration["Database:ConnectionStrings:DefaultConnection"]!);

    DBInitializer.Initialize(app.Configuration["Database:ConnectionStrings:DefaultConnection"]!);
}

app.Run();
