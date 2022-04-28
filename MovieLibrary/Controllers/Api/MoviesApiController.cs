using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Data;

namespace MovieLibrary.Controllers.Api
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly MovieLibraryDbContext data;

        public MoviesApiController(MovieLibraryDbContext data) 
            => this.data = data;
    }
}
