using LearnKeyedServices.Implementation;
using LearnKeyedServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register multiple services for the ICustomLogger interface with different keys
builder.Services.AddKeyedScoped<ICustomLogger, FileLogger>("file");
builder.Services.AddKeyedScoped<ICustomLogger, DatabaseLogger>("database");
builder.Services.AddKeyedScoped<ICustomLogger, EventLogger>("event");

// Add services to the container.

// I don't know why Kanjilal added Controllers here. There are no controllers in this project. But I'll follow his lead.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProductionDbContext>(builder.Configuration["ConnectionStrings:DefaultConnection"]);

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapGet("/customlogger/file", ([FromKeyedServices("file")] ICustomLogger fileLogger) =>
{
    fileLogger.Log("This text is written to the file system.");
    return Results.Ok("File logger executed successfully.");
});

app.MapGet("/customlogger/db", ([FromKeyedServices("database")] ICustomLogger databaseLogger) =>
{
    databaseLogger.Log("This text is stored in the database.");
    return Results.Ok("Database logger executed successfully.");
});

app.MapGet("/customlogger/event", ([FromKeyedServices("event")] ICustomLogger logger) =>
{
    logger.Log("This text is recorded in the event system.");
    return Results.Ok("Event logger executed successfully.");
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

