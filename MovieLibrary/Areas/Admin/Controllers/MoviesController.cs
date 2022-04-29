using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieLibrary.Areas.Admin.Controllers
{
    public class MoviesController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
