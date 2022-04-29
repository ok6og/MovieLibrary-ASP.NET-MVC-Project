using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Data;
using MovieLibrary.Data.Models;
using MovieLibrary.Infrastructure;
using MovieLibrary.Models;
using MovieLibrary.Models.Movies;
using MovieLibrary.Services.Movies;
using System.Security.Claims;

namespace MovieLibrary.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movies;
        private readonly MovieLibraryDbContext data;

        public MoviesController(MovieLibraryDbContext data, IMovieService movies)
        {
            this.data = data;
            this.movies = movies;
        }
           



        public IActionResult All([FromQuery] AllMoviesQueryModel query)
        {
            var queryResult = this.movies.All(
                query.Genre,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllMoviesQueryModel.MoviesPerPage);

            var movieGenres = this.movies.AllMovieGenres();
            
            query.TotalMovies = queryResult.TotalMovies;
            query.Genres = movieGenres;
            query.Movies = queryResult.Movies;

            return View(query);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsTicketSeller())
            {
                return RedirectToAction(nameof(TicketSellerController.Become), "TicketSeller");
            }

            return View(new AddMovieFormModel
            {
                Genres = this.GetMovieGenres()
            });
        }
            

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddMovieFormModel movie)
        {
            var ticketSellerId = this.data
                .TicketSeller
                .Where(t => t.UserId == this.User.GetId())
                .Select(t => t.Id)
                .FirstOrDefault();

            if (ticketSellerId == 0)
            {
                return RedirectToAction(nameof(TicketSellerController.Become), "TicketSeller");
            }
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
                GenreId = movie.GenreId,
                TicketSellerId = ticketSellerId
            };

            this.data.Movies.Add(movieData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsTicketSeller()
            => this.data
                .TicketSeller
                .Any(t => t.UserId == this.User.GetId());


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
