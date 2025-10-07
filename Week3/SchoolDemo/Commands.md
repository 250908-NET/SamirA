## create a new project

`dotnet new webapi -n <project>`

Validate that the databse is up and running

## add packages to the project

`dotnet add package Microsoft.EntityFrameworkCore.Tools`  
`dotnet add package Microsoft.EntityFrameworkCore.Design`  
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`

## tool install for dotnet

`dotnet new tool-manifest`  
`dotnet tool install --local dotnet-ef`  

## scaffold the database

connection string - "Server=localhost,1433;Database=Chinook;User Id=sa;Password=Passw0rd123;"  
provider - Microsoft.EntityFrameworkCore.SqlServer

`dotnet ef dbcontext scaffold \`  
`"Server=localhost,1433;Database=Chinook;User Id=sa;Password=Passw0rd123;TrustServerCertificate=True" \`  
`Microsoft.EntityFrameworkCore.SqlServer \`  
`-o ./Models \`  
`-c ChinookContext`

## migrating to the database

`dotnet ef migrations add <migration_name>`  
`dotnet ef database update`  

## Authorixation and Authentication

### Add identity service

`dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore`

### update classes

- The user type needs to inherit from IdentityUser class
- The DbContext needs to inherit from the IdentityDbContext<*YourUserClass*>
- The Program.cs needs to have the identity service added, along with password, lockout, attemps, and uniqueness conditons
- The Program.cs needs to have "app.UseAuthentication()" and "app.UseAuthorization()" added before "app.Run()", *IN THAT ORDER*

*Remember that updating the context or models will require a new database migration before it can be used.*

## Add JSON Web Tokens (JWTs) for request security

### Add the package

`dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer`

### Configure your API

``` JSON
// appsettings.json
{
  "Logging": {
    // ... your existing logging config
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForSecurity!",
    "Issuer": "<Application_Name>",
    "Audience": "<Application_API_Users>",
    "ExpirationMinutes": 60
  }
}
```

You will also need to update your Program.cs to:

- Read your key information from the appsettings.json
- Implement an Authentication Service (AddAuthentication), and configure it for JWT bearer tokens
- Configure authorization policies (if you want policies as opposed to user-type)

You may also want to configure Swashbuckle and the SwaggerUI to include Authorization so that you can test your endpoints. You'll need to add options to the AddSwaggerGen() service, providing options and configuration on how to identify the token, and how to handle it once entered into the UI.