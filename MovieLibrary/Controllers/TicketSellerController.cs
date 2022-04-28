using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Data;
using MovieLibrary.Data.Models;
using MovieLibrary.Infrastructure;
using MovieLibrary.Models.TicketSeller;

namespace MovieLibrary.Controllers
{
    public class TicketSellerController : Controller
    {
        private readonly MovieLibraryDbContext data;

        public TicketSellerController(MovieLibraryDbContext data) 
            => this.data = data;

        [HttpGet]
        [Authorize]        
        public IActionResult Become()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeTicketSellerFormModel seller)
        {
            var userId = this.User.GetId();
            var userIdAlreadySeller = this.data
                .TicketSeller
                .Any(t => t.UserId == userId);

            if (userIdAlreadySeller)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(seller);
            }

            var sellerData = new TicketSeller
            {
                Name = seller.Name,
                PhoneNumber = seller.PhoneNumber,
                UserId = userId

            };

            this.data.TicketSeller.Add(sellerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Movies");
        }


    }
}
