using MovieLibrary.Data;
using MovieLibrary.Data.Models;
using MovieLibrary.Services.TicketSellers;
using MovieLibrary.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieLibrary.Test.Services
{
    public class TicketSellerServiceTest
    {
        private const string UserId = "testId";
        [Fact]
        public void IsTicketSellerShouldReturnTrueWhenUserIsTicketSeller()
        {
            using var data = GetTicketSellerData();
            
            var ticketSellerService = new TicketSellerService(data);

            var result = ticketSellerService.IsTicketSeller(UserId);

            Assert.True(result);
        }

        [Fact]
        public void IsDealerShouldReturnFalseWhenUserIsNotDealer()
        {

            var data = GetTicketSellerData();
            var ticketSellerService = new TicketSellerService(data);

            var result = ticketSellerService.IsTicketSeller("SomeoneElse");

            Assert.False(result);
        }

       private static MovieLibraryDbContext GetTicketSellerData()
        {

            var data = DatabaseMock.Instance;

            data.TicketSeller.Add(new TicketSeller
            {
                UserId = UserId,
                Name = "gosho",
                PhoneNumber = "9999"
            });
            data.SaveChanges();

            return data;


        }
    }
}
