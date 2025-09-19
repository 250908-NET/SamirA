using System;
using Serilog;

namespace ToDoApp
{
    class Program
    {
        static void Main(string[] args) 
        {
            // Console.WriteLine("Hello, World!"); // from our console app... bye bye! 

            // Serilog - a logging library for .NET applications
            // dotnet add <project .csproj> package <package name>
            // dotnet add ToDoApp.csproj package Serilog.AspNetCore
            // dotnet add package Serilog.Sinks.Console

            // logging sinks - where the logs go
            // console, file, database, remote server, etc.

            var builder = WebApplication.CreateBuilder(args); // Create a builder for the web application
           
           // configure logging before we "build" the app
           Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
           builder.Host.UseSerilog(); // Use Serilog for logging
           
           
           var app = builder.Build(); // Build the web application
            app.MapGet("/", (ILogger<Program> logger) => {
                logger.LogInformation("Hello World endpoint was called"); // Log an information message
                return "Hello World!";
            }); // Map a GET request to the root URL to return "Hello World!"

            // HTTP requests - responses have body, and headers
            // a HEAD request is like a GET request but without the  - the metadata

            // Put, Post, Patch - include a body
            // Get, Delete, Head, Options - do not include a body

            app.Run(); // Run the web application




            // LOGGING - record the function/activity/behaviors/events of an application
            // events - requests, responses, system/application status, errors, warnings, crashes/shutdowns, startup

            // levels of logging:
            // Trace - most detailed, used for diagnosing issues - everything that happens!
            // Debug - less detailed, used for debugging issues - things that are useful to developers
            // Information - general information about the application's operation - things that are useful to users
            // ----- this is the last "everything is ok" level
            // Warning - something unexpected happened, but the application is still running - things that might require attention
            // Error - something went wrong, the application might be unable to perform a function - things that require immediate attention
            // Critical - something went very wrong, the application might be unable to continue running - things that require immediate attention and action



        }
    }
}