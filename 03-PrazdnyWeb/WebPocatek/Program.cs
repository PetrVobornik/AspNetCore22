using Microsoft.EntityFrameworkCore;
using WebPocatek.Data;

// Vytvoøení vytvaøeèe (buildera) aplikace
var builder = WebApplication.CreateBuilder(args);

// Servis pro Razor Pages
builder.Services.AddRazorPages();

// Servis pro DB - SQL Server
builder.Services.AddDbContext<ToDoDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
);

// Vytvoøení aplikace
var app = builder.Build();

// Migrace databáze
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
    db.Database.Migrate();
}

// Zpøístupnit statické soubory ve složce wwwroot
app.UseStaticFiles();

// Zmapovat stránky webu
app.MapRazorPages();

// Spustit aplikace
app.Run();
