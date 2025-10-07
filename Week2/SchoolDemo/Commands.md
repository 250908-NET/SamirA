*create a new project
dotnet new webapi -n <project>

*validate that the databse is up and running

*add packages to the project
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

*tool install for dotnet
dotnet new tool-manifest
dotnet tool install --local dotnet-ef

*scaffold the database
connection string - "Server=localhost,1433;Database=Chinook;User Id=sa;Password=Passw0rd123;"
provider - Microsoft.EntityFrameworkCore.SqlServer

dotnet ef dbcontext scaffold "Server=localhost,1433;Database=Chinook;User Id=sa;Password=Passw0rd123;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o ./Models -c ChinookContext

*migrating to the database
dotnet ef migrations add <migration_name>
dotnet ef database update
