using Microsoft.EntityFrameworkCore;
using UserSafetyAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Registrera DbContext med SQL Server LocalDB
builder.Services.AddDbContext<AppDbContext> (options =>
    options.UseSqlServer (builder.Configuration.GetConnectionString ("DefaultConnection")));

var app = builder.Build();
//Automatisk migrering vid uppstart
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
