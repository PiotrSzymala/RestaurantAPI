using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using RestaurantAPI;
using RestaurantAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

builder.Services.AddDbContext<RestaurantApiContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantApiConnectionString"))
    );

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<RestaurantApiContext>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

//DataGenerator.Seed(dbContext);
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

NLog.LogManager.Shutdown();
