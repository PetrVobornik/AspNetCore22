using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAPI.Shared;

namespace BlazorAPI.Data;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Ukol> Ukoly => Set<Ukol>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ukol>().HasData(
            new Ukol() { Id = 1, Nazev = "Nakoupit", Popis = "3 rohlíky, 1/2 chleba", Termin = DateTime.Today },
            new Ukol() { Id = 2, Nazev = "Uklidit", Popis = "Doma a venku", Termin = DateTime.Today.AddDays(7) },
            new Ukol() { Id = 3, Nazev = "Opravit kolo", Popis = "Je tam díra", Termin = DateTime.Today.AddDays(16) },
            new Ukol() { Id = 4, Nazev = "Vytvořit si web", Popis = "Vlastní web s kontakty na mě" },
            new Ukol() { Id = 5, Nazev = "Přečíst si knihu", Termin = DateTime.Today.AddDays(-1), Hotovo = true },
            new Ukol() { Id = 6, Nazev = "Úkol z matematiky", Termin = DateTime.Today.AddDays(-7), Hotovo = true }
            );
    }
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ToDoDbContext>
{
    public ToDoDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ToDoDbContext>();
        builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ToDoApi_design;Trusted_Connection=True;MultipleActiveResultSets=true");
        return new ToDoDbContext(builder.Options);
    }
}
