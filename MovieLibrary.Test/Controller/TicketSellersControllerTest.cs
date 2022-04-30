using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieLibrary.Controllers;
using MovieLibrary.Models.TicketSeller;
using MovieLibrary.Services.Movies;
using MovieLibrary.Services.Statistics;
using MovieLibrary.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieLibrary.Test.Controller
{
    public class TicketSellersControllerTest
    {
        [Fact]
        public void BecomeShouldReturnViewWithCorrectModel()
        {

            var data = DatabaseMock.Instance;
            var homeController = new TicketSellersController(data);


            var result = homeController.Become();
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllShouldReturnView()
        {
            var data = DatabaseMock.Instance;
            var moviesController = new TicketSellersController(data);
            var model = new BecomeTicketSellerFormModel
            {
                Name = "gosho",
                PhoneNumber = "04434423"
            };
            moviesController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = ClaimsPrincipalMock.Instance()
            };

            var result = moviesController.Become(model);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
