using Microsoft.EntityFrameworkCore;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    // Use SQL SERVER db in production and sqlite db in development
    // Note: For now SQLite in both Dev + Prod
    //if (env.IsProduction())
    //   services.AddDbContext<DataContext>();
    //else
    //    services.AddDbContext<DataContext, SqliteDataContext>();
    
    // Connection String for local SQLite
    //  "WebApiDatabase": "Data Source=LocalDatabase.db"
    
    // The DB at MS SQL SERVER needs to be created by running the Web API locally with a NEW Migration / Initial Create
    // Migration to MS SQL Server
     services.AddDbContext<DataContext>();

    services.AddCors();
    services.AddControllers();

    // configure automapper with all automapper profiles from this assembly
    services.AddAutoMapper(typeof(Program));

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();
}

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();    
    dataContext.Database.Migrate();
}

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}



// Note: For production (simply.com) it needs to be without localhost !
app.Run();

// Only for local developement !
//app.Run("http://localhost:4000");