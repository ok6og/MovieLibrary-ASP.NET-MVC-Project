using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MovieLibrary.Models;
using MovieLibrary.Data;
using MovieLibrary.Models.Home;
using MovieLibrary.Services.Statistics;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace MovieLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly MovieLibraryDbContext data;
        private readonly IMapper mapper;

        public HomeController(
            IStatisticsService statistics,
            MovieLibraryDbContext data,
            IMapper mapper)
        {
            this.statistics = statistics;
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {

            var movies = this.data
                .Movies
                .OrderByDescending(m => m.Id)
                .ProjectTo<MovieIndexViewModel>(this.mapper.ConfigurationProvider)
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