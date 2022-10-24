using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogovaciWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser, BlogRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Komentar>()
                .HasOne(x => x.Blog)
                .WithMany(x => x.Komentare)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BlogItem>()
                .HasMany(x => x.Komentare)
                .WithOne(x => x.Blog)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BlogUser>()
                .HasIndex(x => x.NickName)
                .IsUnique();

            base.OnModelCreating(builder);
        }

        public DbSet<BlogItem> Blog => Set<BlogItem>();
        public DbSet<Komentar> Komentare => Set<Komentar>();
    }
}