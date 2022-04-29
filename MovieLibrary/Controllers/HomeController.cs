using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MovieLibrary.Models;
using MovieLibrary.Data;
using MovieLibrary.Models.Home;
using MovieLibrary.Services.Statistics;

namespace MovieLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly MovieLibraryDbContext data;

        public HomeController(
            IStatisticsService statistics,
            MovieLibraryDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
            
            var movies = this.data
                .Movies
                .OrderByDescending(m => m.Id)
                .Select(c => new MovieIndexViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    RuntimeInMinutes = c.RuntimeInMinutes,                    
                    Year = c.Year,
                })
                .Take(3)
                .ToList();

            var totalStatistics = this.statistics.Total();


            return View(new IndexViewModel
            {
                TotalMovies = totalStatistics.TotalMovies,
                TotalUsers = totalStatistics.TotalUsers,
                Movies = movies


            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}