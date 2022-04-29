using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieLibrary.Areas.Admin.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = WebConstants.AdministratorRoleName)]
    public class AdminController : Controller
    {
    }
}
