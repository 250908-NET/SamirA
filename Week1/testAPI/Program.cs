using Serilog; // logging library
using Microsoft.AspNetCore.Mvc; // for [Annontations]
using Models.Car; // using the Car

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

// endpoint methods

app.MapGet("/", (ILogger<Program> logger) => 
{
    logger.LogTrace("This is a trace log");
    logger.LogDebug("This is a debug log");
    logger.LogInformation("Hello from the root endpoint");
    logger.LogWarning("This is a warning log");
    logger.LogError("This is an error log");
    logger.LogCritical("This is a critical log");

    return "Hello World!";
}).WithName("GetRoot");

// /echo?message=hello%20world
// classes will always default to being read from the body of a message
app.MapPost("/echo", ( [FromBody] string message, ILogger<Program> logger) => 
{
    logger.LogInformation("Echoing message: {Message}", message);
    return Results.Ok(message);
}).WithName("PostEcho");


// Car Functionality

// Car[] cars = new Car[]; // array - fixed size 
List<Car> carList = new List<Car>(); // list - dynamic size and method management 

app.MapPost("/car", (ILogger<Program> logger, [FromBody] Car car) =>
{
    logger.LogInformation($"Run the post car method {car}");
    carList.Add(car);
}).WithName("Create Car");

app.MapGet("/car", (ILogger<Program> logger) =>
{
    logger.LogInformation("Run the get all cars method");
    return Results.Ok(carList);
}).WithName("Get All Cars");

app.MapGet("/car/{id}", (ILogger<Program> logger, [FromRoute] int id) =>
{
    logger.LogInformation($"Run the get car by id method: {id}");
    /* For loop implementation
    for (int i = 0; i < carList.Count; i++)
    {
        if (carList[i].Id == id)
        {
            return Results.Ok(carList[i]);
        }
    }
    */

    // LINQ - Language Integrated Query
    
    /* all iterables/enumerables
    Elements    a   b   c   d
    indexes     0   1   2   3
    */

    // return Results.Ok(carList.ElementAt(id)); // get by index - not what we want
    
    // Anonymous function/labmda expression syntax
    // ( returning => (expressions))
    return Results.Ok(carList.Where(car => car.Id == id).ToList()); // returns a list of cars that match the id


}).WithName("Get Car By Id");

app.Run();