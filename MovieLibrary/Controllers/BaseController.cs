using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieLibrary.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
