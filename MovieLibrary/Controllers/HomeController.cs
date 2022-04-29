using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Services.Movies;
using Microsoft.Extensions.Caching.Memory;

using static MovieLibrary.WebConstants.Cache;

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
            var latestMovies = this.cache.Get<List<LatestMoviesServiceModel>>(LatestMoviesCacheKey);

            if (latestMovies == null)
            {
                latestMovies = this.movies.Latest().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestMoviesCacheKey, latestMovies, cacheOptions);
            }
            return View(latestMovies);
        }


        public IActionResult Error() => View();
    }
}