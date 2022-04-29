using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Services.Movies;
using Microsoft.Extensions.Caching.Memory;

namespace MovieLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService movies;
        private readonly IMemoryCache cache;

        public HomeController(
            IMovieService movies,
            IMemoryCache cache)
        {
            this.movies = movies;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string latestMoviesCacheKey = "LatestMoviesCacheKey";

            var latestMovies = this.cache.Get<List<LatestMoviesServiceModel>>(latestMoviesCacheKey);

            if (latestMovies == null)
            {
                latestMovies = this.movies.Latest().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestMoviesCacheKey, latestMovies, cacheOptions);
            }
            return View(latestMovies);
        }


        public IActionResult Error() => View();
    }
}