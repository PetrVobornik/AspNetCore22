using Microsoft.EntityFrameworkCore;
using WebPocatek.Data;

// Vytvo�en� vytva�e�e (buildera) aplikace
var builder = WebApplication.CreateBuilder(args);

// Servis pro Razor Pages
builder.Services.AddRazorPages();

// Servis pro DB - SQL Server
builder.Services.AddDbContext<ToDoDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
);

// Vytvo�en� aplikace
var app = builder.Build();

// Migrace datab�ze
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
    db.Database.Migrate();
}

// Zp��stupnit statick� soubory ve slo�ce wwwroot
app.UseStaticFiles();

// Zmapovat str�nky webu
app.MapRazorPages();

// Spustit aplikace
app.Run();
