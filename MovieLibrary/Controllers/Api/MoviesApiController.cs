using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Api.Movies;
using MovieLibrary.Services.Movies;


namespace MovieLibrary.Controllers.Api
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieService movies;

        public MoviesApiController(IMovieService movies) 
            => this.movies = movies;


        [HttpGet]
        public MovieQueryServiceModel All([FromQuery] AllMoviesApiRequestModel query) 
            => this.movies.All(
                query.Genre,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.MoviesPerPage);

    }
}
