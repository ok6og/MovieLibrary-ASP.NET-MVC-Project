using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Data;
using MovieLibrary.Data.Models;
using MovieLibrary.Models.Movies;

namespace MovieLibrary.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieLibraryDbContext data;

        public MoviesController(MovieLibraryDbContext data)
            => this.data = data;

        [HttpGet]
        public IActionResult Add() => View(new AddMovieFormModel
        {
            Genres = this.GetMovieGenres()
        });

        public IActionResult All()
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
                .ToList();

            return View(movies);
        }

        [HttpPost]
        public IActionResult Add(AddMovieFormModel movie)
        {
            if (!this.data.Genres.Any(g => g.Id == movie.GenreId))
            {
                this.ModelState.AddModelError(nameof(movie.GenreId), "Genre does not exist.");
            }
            if (!ModelState.IsValid)
            {
                movie.Genres = this.GetMovieGenres();
                return View(movie);
            }

            var movieData = new Movie
            {
                Title = movie.Title,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl,
                Year = movie.Year,
                RuntimeInMinutes = movie.RuntimeInMinutes,
                GenreId = movie.GenreId
            };
            this.data.Movies.Add(movieData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<MovieGenreViewModel> GetMovieGenres()
            => this.data
            .Genres
            .Select(x => new MovieGenreViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();
    }
}
