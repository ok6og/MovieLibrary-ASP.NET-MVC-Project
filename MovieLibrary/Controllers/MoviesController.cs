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



        public IActionResult All([FromQuery] AllMoviesQueryModel query)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Genre))
            {
                moviesQuery = moviesQuery.Where(m => m.Genre.Name == query.Genre);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                moviesQuery = moviesQuery.Where(m =>
                    m.Title.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    m.Year.ToString().ToLower().Contains(query.SearchTerm.ToLower()) ||
                    m.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            moviesQuery = query.Sorting switch
            {

                MovieSorting.Title => moviesQuery.OrderBy(m => m.Title),
                MovieSorting.Year => moviesQuery.OrderByDescending(m => m.Year),
                MovieSorting.Runtime => moviesQuery.OrderBy(m => m.Id),
                MovieSorting.DateCreated or _ => moviesQuery.OrderByDescending(m => m.Id)
            };
            var totalMovies = moviesQuery.Count();

            var movies = moviesQuery
                .Skip((query.CurrentPage - 1) * AllMoviesQueryModel.MoviesPerPage)
                .Take(AllMoviesQueryModel.MoviesPerPage)
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
            var movieGenres = this.data
                .Movies
                .Select(m => m.Genre.Name)
                .Distinct()
                .OrderBy(g => g)
                .ToList();

            query.TotalMovies = totalMovies;
            query.Genres = movieGenres;
            query.Movies = movies;

            return View(query);
        }
        [HttpGet]
        public IActionResult Add() => View(new AddMovieFormModel
        {
            Genres = this.GetMovieGenres()
        });

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
