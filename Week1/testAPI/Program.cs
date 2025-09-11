using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Add packages to the .NET project
// dotnet add TodoApi.csproj package Swashbuckle.AspNetCore -v 6.6.2
// dotnet - using something in the dotnet toolbox
// add - add a package
// TodoApi.csproj - the project file to add the package to
// package - the type of thing to add
// Swashbuckle.AspNetCore - the name of the package to add
// -v 6.6.2 - the version of the package to add

// HTTP Response Codes ********************
// 1xx - informational
// 2xx - successfull
// 3xx - redirections
// 4xx - client side error - 404 - bad request, 418 - I am a teapot
// 5xx - server side errors - 513 - internal server error

// OPEN API / SWAGGER ********************
// OpenAPI and Swagger setup
// Swashbuckle.AspNetCore -v 6.6.2
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// LOGGING ********************

// there are 6 levels of logging
// Trace - most detailed, used for diagnosing issues
// Debug - detailed information, useful for debugging
// Information - general information about application flow - our highest level of "everything is ok"
// Warning - something unexpected, but the application is still working
// Error - something failed, the application might be able to recover
// Critical - something very bad happened, the application might be shutting down - "it's dead jim"

// Adding logging
// Serilog
// Serilog.AspNetCore
// Serilog.Sinks.Console
// Serilog.Sinks.File

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger(); // read from appsettings.json
/*/Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger(); // configure in place
*/
builder.Host.UseSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
// ONLY if we're in Development...
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (ILogger<Program> logger) =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    logger.LogInformation("Weather forecast generated with {Count} entries", forecast.Length);
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/", (ILogger<Program> logger) => 
{
    logger.LogTrace("This is a trace log");
    logger.LogDebug("This is a debug log");
    logger.LogInformation("Hello from the root endpoint");
    logger.LogWarning("This is a warning log");
    logger.LogError("This is an error log");
    logger.LogCritical("This is a critical log");

    return "Hello World!";
});

app.MapPost("/echo", (string message, ILogger<Program> logger) => 
{
    logger.LogInformation("Echoing message: {Message}", message);
    return Results.Ok(message);
});





app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
