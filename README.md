# dotnet-6-signup-mssql-api

.NET 6.0 - User Registration and Login Tutorial with Example API

# Migration - Initial Create locally towards SQLite
# Make sure that the connection String points to the SQLite ( locally ) in the "appsettings.Development" 

"WebApiDatabase": "Data Source=LocalDatabase.db"

dotnet ef migrations add InitialCreate --context SqliteDataContext --output-dir Migrations/SqliteMigrations 

# Migration - Initial Create locally towards a remote MS SQL Server

Change the connection String to the remote MS SQL Server in the "appsettings.Development" for these purpose

Note: When running the Web API locally it will use "appsettings.Development" 
and not "appsettings" which must also have the MS SQL Server connection String

set ASPNETCORE_ENVIRONMENT=Production
dotnet ef migrations add InitialCreate --context DataContext --output-dir Migrations/SqlServerMigrations
