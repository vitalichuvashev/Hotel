Database installation

1. Attach Hotel.mdf database to SQL server, or create new if you use SQL express(look below how create database from scratch).
Hotel.mdf in the root of project folder.


2. If you need create database from scratch.


2.1 Open solution Hotell.sln with Visual Studio 2022, 
right mouse click on Hotel.Repository and install with Nuget package manager
-- "Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0"
-- "Microsoft.EntityFrameworkCore.Tools" Version="8.0.0"


2.2 Right mouse click on project Hotel.Repository, choose "Set as Startup Project" 
2.3 Open file DbContext.cs, and change connection string inside method "void OnConfiguring(DbContextOptionsBuilder options);"
IMPORTANT! 
This connection string is used by Entity framework designer when you create new, clean database,it is NOT used by WebApi application !

2.4 Inside Visual Studio open Package Manger Console window, choose Hotel.Repository in default project dropdown.
2.5 Run commands
--add-migration <name>
--update-database

2.6 Database will be created with data seed(4 users, 16 hotell rooms)
--------------------------------------------------------------------------------------


Install WebApi application on IIS

1. Don't forget install .NET Core hosting bundle
https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-8.0#iis-configuration

2. Install web api rest service 
https://www.c-sharpcorner.com/article/hosting-asp-net-web-api-rest-service-on-iis-10/

3. Change connection string in appsettings.json, connection string injected by DI into Hotel.Repository.dll
when WebApi application is started.

4. Change WebApi host address in Hotel.WebApi.http, if required
----------------------------------------------------------------------------------------


Install Hotel web application on IIS

1. Publish an ASP.NET Core app to IIS 
https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-8.0&tabs=visual-studio

2. Change WebApiUrl inside appsettings.json, url MUST be same as for Hotel.WebApi.http in WebApi application described above. 


