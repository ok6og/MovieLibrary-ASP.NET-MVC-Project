using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Infrastructure;
using MovieLibrary.Models.Movies;
using MovieLibrary.Services.Movies;
using MovieLibrary.Services.TicketSellers;

namespace MovieLibrary.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movies;
        private readonly ITicketSellerService ticketSeller;


        public MoviesController(IMovieService movies, ITicketSellerService ticketSeller)
        {
            this.movies = movies;
            this.ticketSeller = ticketSeller;
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

        [Authorize]
        public IActionResult Mine()
        {
            var myMovies = this.movies.ByUser(this.User.Id());
            return View(myMovies);
        }


        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            if (!this.ticketSeller.IsTicketSeller(this.User.Id()))
            {
                return RedirectToAction(nameof(TicketSellersController.Become), "TicketSeller");
            }

            return View(new MovieFormModel
            {
                Genres = this.movies.AllGenres()
            });
        }
            

        [HttpPost]
        [Authorize]
        public IActionResult Add(MovieFormModel movie)
        {
            var ticketSellerId = this.ticketSeller.IdByUser(this.User.Id());

            if (ticketSellerId == 0)
            {
                return RedirectToAction(nameof(TicketSellersController.Become), "TicketSeller");
            }
            if (!this.movies.GenreExists(movie.GenreId))
            {
                this.ModelState.AddModelError(nameof(movie.GenreId), "Genre does not exist.");
            }
            if (!ModelState.IsValid)
            {
                movie.Genres = this.movies.AllGenres();
                return View(movie);
            }

            this.movies.Create(
                movie.Title,
                movie.Description,
                movie.ImageUrl,
                movie.Year,
                movie.RuntimeInMinutes,
                movie.GenreId,
                ticketSellerId);

            return RedirectToAction(nameof(All));
        }
        [Authorize]
        public  IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.ticketSeller.IsTicketSeller(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(TicketSellersController.Become), "TicketSeller");
            }

            var movie = this.movies.Details(id);

            if(movie.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new MovieFormModel
            {
                Title = movie.Title,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl,
                Year = movie.Year,
                RuntimeInMinutes = movie.RuntimeInMinutes,
                GenreId = movie.GenreId,
                
                Genres = this.movies.AllGenres()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, MovieFormModel movie)
        {
            var ticketSellerId = this.ticketSeller.IdByUser(this.User.Id());

            if (ticketSellerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(TicketSellersController.Become), "TicketSeller");
            }
            if (!this.movies.GenreExists(movie.GenreId))
            {
                this.ModelState.AddModelError(nameof(movie.GenreId), "Genre does not exist.");
            }
            if (!ModelState.IsValid)
            {
                movie.Genres = this.movies.AllGenres();
                return View(movie);
            }

            if (!this.movies.IsByTicketSeller(id,ticketSellerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.movies.Edit(
                id,
                movie.Title,
                movie.Description,
                movie.ImageUrl,
                movie.Year,
                movie.RuntimeInMinutes,
                movie.GenreId);

            
            return RedirectToAction(nameof(All));
        }
    }
}
