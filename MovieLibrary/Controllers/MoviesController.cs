using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Infrastructure.Extensions;
using MovieLibrary.Models.Movies;
using MovieLibrary.Services.Movies;
using MovieLibrary.Services.TicketSellers;

using static MovieLibrary.WebConstants;

namespace MovieLibrary.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movies;
        private readonly ITicketSellerService ticketSeller;
        private readonly IMapper mapper;


        public MoviesController(IMovieService movies, ITicketSellerService ticketSeller, IMapper mapper)
        {
            this.movies = movies;
            this.ticketSeller = ticketSeller;
            this.mapper = mapper;
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

        public IActionResult Details(int id, string information)
        {
            var movie = this.movies.Details(id);
            var movieInfo = movie.GetInformation();

            if (information != movieInfo)
            {
                return BadRequest();
            }
            return View(movie);
        }


        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            if (!this.ticketSeller.IsTicketSeller(this.User.Id()))
            {
                return RedirectToAction(nameof(TicketSellersController.Become), "TicketSellers");
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
                return RedirectToAction(nameof(TicketSellersController.Become), "TicketSellers");
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

            var movieId = this.movies.Create(
                movie.Title,
                movie.Description,
                movie.ImageUrl,
                movie.Year,
                movie.RuntimeInMinutes,
                movie.GenreId,
                ticketSellerId);

            TempData[GlobalMessageKey] = "Your movie was successfully added and is waiting for approval!";

            return RedirectToAction(nameof(Details), new {id= movieId, information = movie.GetInformation() });
        }
        [Authorize]
        public  IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.ticketSeller.IsTicketSeller(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(TicketSellersController.Become), "TicketSellers");
            }

            var movie = this.movies.Details(id);

            if(movie.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }
            var movieForm = this.mapper.Map<MovieFormModel>(movie);
            movieForm.Genres = this.movies.AllGenres();
            return View(movieForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, MovieFormModel movie)
        {
            var ticketSellerId = this.ticketSeller.IdByUser(this.User.Id());

            if (ticketSellerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(TicketSellersController.Become), "TicketSellers");
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
                movie.GenreId,
                this.User.IsAdmin());

            TempData[GlobalMessageKey] = $"Movie wassuccessfully edited {(this.User.IsAdmin() ? string.Empty : "and is waiting for approval!")}";

            return RedirectToAction(nameof(Details), new { id, information = movie.GetInformation() });
        }
    }
}
