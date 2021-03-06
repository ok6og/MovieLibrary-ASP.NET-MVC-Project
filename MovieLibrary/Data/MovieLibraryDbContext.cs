using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Data
{
    public class MovieLibraryDbContext : IdentityDbContext<User>
    {      
        public MovieLibraryDbContext(DbContextOptions<MovieLibraryDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; init; }
        public DbSet<Genre> Genres { get; init; }
        public DbSet<TicketSeller> TicketSeller { get; init; }
        public DbSet<Actor> Actors { get; init; }
        public DbSet<ActorMovie> ActorsMovies { get; init; }


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
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<TicketSeller>(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ActorMovie>()
                .HasKey(am => new
                {
                    am.ActorId,
                    am.MovieId
                });
            builder
                .Entity<ActorMovie>()
                .HasOne(m => m.Movie)
                .WithMany(am => am.ActorsMovies)
                .HasForeignKey(m => m.MovieId);

            builder
                .Entity<ActorMovie>()
                .HasOne(m => m.Actor)
                .WithMany(am => am.ActorsMovies)
                .HasForeignKey(m => m.ActorId);


            base.OnModelCreating(builder);
        }
    }
}