# dotnet-6-signup-mssql-api

.NET 6.0 - User Registration and Login Tutorial with Example API

# Functionality of the Web App

- JWT authentication
- CRUD Account management routes with role based access control

# Tech used for creating the Web App

- A .NET 6 Web API
- An Angular 14 Web Client for the Frontend
- Entity Framework
- SQLite as a local DB
- A traditional Webhotel for hosting
- MS SQL for remote DB
- VS Code
- Azure Data Studio

# Updated EF Core tool to the latest version - Version 8.03
dotnet tool update --global dotnet-ef

# Development
# Create the Initial Migration for SQLite DB - should work for any DB
set ASPNETCORE_ENVIRONMENT=Development

Make sure that the connection String points to the SQLite ( locally ) in the "appsettings.Development" 

"WebApiDatabase": "Data Source=LocalDatabase.db"

dotnet ef migrations add InitialCreate --context SqliteDataContext --output-dir Migrations/SqliteMigrations 

# Production
# Migration - Initial Create locally towards a remote MS SQL Server

Change the connection String to the remote MS SQL Server in the "appsettings.Development" for these purpose

Note: When running the Web API locally it will use "appsettings.Development" 
and not "appsettings" which must also have the MS SQL Server connection String

set ASPNETCORE_ENVIRONMENT=Production
dotnet ef migrations add InitialCreate --context DataContext --output-dir Migrations/SqlServerMigrations

# Create the remote SQLite DB at the remote server
https://remote-host.com/users
