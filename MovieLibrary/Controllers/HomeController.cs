using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MovieLibrary.Models;
using MovieLibrary.Data;
using MovieLibrary.Models.Movies;

namespace MovieLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieLibraryDbContext data;

        public HomeController(MovieLibraryDbContext data)
            => this.data = data;
        public IActionResult Index()
        {
            var movies = this.data
                .Movies
                .OrderByDescending(m => m.Id)
                .Select(c => new MovieListingViewModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    Genre = c.Genre.Name,
                    ImageUrl = c.ImageUrl,
                    RuntimeInMinutes = c.RuntimeInMinutes,
                    Title = c.Title,
                    Year = c.Year,
                })
                .Take(3)
                .ToList();

            return View(movies);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}