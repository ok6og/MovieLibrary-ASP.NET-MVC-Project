using MovieLibrary.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Models;
using Microsoft.AspNetCore.Identity;
using static MovieLibrary.WebConstants;

namespace MovieLibrary.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);


            SeedGenres(services);
            SeedAdministrator(services);
            
            return app;
        }
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<MovieLibraryDbContext>();
            data.Database.Migrate();
        }
        private static void SeedGenres(IServiceProvider services)
        {
            var data = services.GetRequiredService<MovieLibraryDbContext>();
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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async() =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }
                var role = new IdentityRole { Name = AdministratorRoleName };
                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@imdb.com";
                const string adminPassword = "theADMIN";


                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };
                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();

        }

    }
}
