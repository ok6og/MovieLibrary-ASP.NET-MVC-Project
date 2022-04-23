using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Infrastructure.Data;

namespace MovieLibrary.Controllers.Api
{
    [ApiController]
    [Route("api/movies")]
    public class MovieApiController : ControllerBase
    {
        private readonly ApplicationDbContext data;

        public MovieApiController(ApplicationDbContext data)
        {
            this.data = data;
        }



    }
}
