# School Demo Application

## Abstract

This application is currently a .NET minimal api, with functionality focused on the management of a school.
The school is expected to have Students, Instructors, and Courses. As a demonstration of good project architecutre,
the different layers are separated to manage Models and Services separately. To highlight the Repository Pattern,
there is also a set of repository interfaces and implementations to handle all database interactions.

Persistance is provided through a SQL Server database, which can be established and connected to by adding a
connection string to the application files. All database communication and management can be accomplished with
Entity Framework Core.

## Startup
- Create a database server
    -- Docker, local, or cloud-based databases will all function for this purpose.
- Create a connection_string.env file in the root directory for the appliction (/SchoolDemo/)
    -- The connection string should be in plain text, and does not need quotation marks or the like
- Install Entity Framework Core on the project
    -- `dotnet new tool-manifest`
    -- `dotnet tool install --local dotnet-ef`
- Update the database with the migrations included in the project
    -- `dotnet ef database update`
- Run the application
    -- `dotnet run --project ./School.Api/School.Api.csproj`
