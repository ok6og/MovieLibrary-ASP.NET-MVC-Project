using Microsoft.AspNetCore.Identity;
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
        public DbSet<TicketSeller> TicketSeller { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany(m => m.Movies)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Movie>()
                .HasOne(m => m.TicketSeller)
                .WithMany(t => t.Movies)
                .HasForeignKey(m => m.TicketSellerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<TicketSeller>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<TicketSeller>(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            

            base.OnModelCreating(builder);
        }
    }
}