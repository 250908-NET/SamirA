namespace WebApi.EndPoints;

public static class Challenge4 {
    public static void DateTimeFun(this IEndpointRouteBuilder app) {
        app.MapGet("/date/today", () => {
            return Results.Ok(new { operation = "date", result = DateTime.Now });
        });

        app.MapGet("/date/age/{birthYear:int}", (int birthYear) =>{
            var thisYear = DateTime.Today.Year;
            var age = thisYear - (int)birthYear;
            return Results.Ok(new {operation = "date", result = age});
        });

    }
}
