using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Data
{
    public class MovieLibraryDbContext : IdentityDbContext
    {      
        public MovieLibraryDbContext(DbContextOptions<MovieLibraryDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; init; }
        public DbSet<Genre> Genres { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany(m => m.Movies)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}