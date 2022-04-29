using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Services.Movies;

namespace MovieLibrary.Areas.Admin.Controllers
{
    public class MoviesController : AdminController
    {
        private readonly IMovieService movies;

        public MoviesController(IMovieService movies) => this.movies = movies;


        public IActionResult All() => View(this.movies.All(publicOnly:false).Movies);
        
        public IActionResult ChangeVisibility(int id)
        {
            this.movies.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
