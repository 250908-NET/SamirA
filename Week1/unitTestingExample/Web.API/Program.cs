var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
// should return: "Hello World!"
// should NOT return: "Hello my name is:"

app.Run();


// public static int method(int a)


// access modifier - how we can ineract with a thing
// public - anyone can see/run/use it
// private - only other members of the same class can see/run/use it
// protected - limits to other members of the same object, or derived objects

// private class Program{}
// {
    public partial class Program {};
// }
// partial - split the definition of a class over multiple files