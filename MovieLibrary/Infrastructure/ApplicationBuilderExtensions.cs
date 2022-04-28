using MovieLibrary.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<MovieLibraryDbContext>();

            data.Database.Migrate();

            SeedGenres(data);
            
            return app;
        }
        private static void SeedGenres(MovieLibraryDbContext data)
        {
            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
            {
                new Genre{ Name = "Action" },
                new Genre{ Name = "Animation" },
                new Genre{ Name = "Comedy" },
                new Genre{ Name = "Crime" },
                new Genre{ Name = "Drama" },
                new Genre{ Name = "Fantasy" },
                new Genre{ Name = "Horror" },
                new Genre{ Name = "Mystery" },
                new Genre{ Name = "Romance" },
                new Genre{ Name = "Thriller" },
                new Genre{ Name = "Western" },                                
                new Genre{ Name = "Other" },
            });

            data.SaveChanges();
        }

    }
}
