using Microsoft.EntityFrameworkCore;
using MovieWEBApp.Models;

namespace MovieWEBApp.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
             .Entity<Movie>()
             .HasMany(p => p.actors)
             .WithMany(p => p.isActor);
            modelBuilder
              .Entity<Movie>()
              .HasMany(p => p.directors)
              .WithMany(p => p.isDirector);
        }
    }

}
