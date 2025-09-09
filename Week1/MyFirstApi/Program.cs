var builder = WebApplication.CreateBuilder(args);
/*
multi-line comment

still a comment
*/

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", null, "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

/* 
HTTP Request Types
- Get - Read
- Post - Create
- Patch - Update(partial)
- Put - Update/Replace
- Delete - Delete
- Head - a get request without a body
- Options - returns the supported methods on an endpoint
*/

/*
HTTP GET v1.1
localhost:5001/weatherforecast
headers
{
    "Accept": "application/json"
    "Response-Type": "application/json"
}

Header
Body
*/

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/", () => "Hello World!");

app.MapGet("/number", () => 
{
    return 42;
});

app.MapGet("/add/{a}/{b}", (int a, int b) => 
{
    return new
    {
        operation = "add",
        inputa = a,
        inputb = b,
        sum = a + b
    };
});


app.Run();
                                   // "" != null
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}