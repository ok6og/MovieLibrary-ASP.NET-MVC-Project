using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Controllers;
using MovieLibrary.Data.Models;
using MovieLibrary.Models;
using MovieLibrary.Models.Movies;
using MovieLibrary.Services.Movies;
using MovieLibrary.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieLibrary.Test.Controller
{
    public class MoviesControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);
            var moviesController = new MoviesController(movieService,null, mapper);
            var movies = new List<MovieServiceModel>();
            var model = new AllMoviesQueryModel
            {
                Movies = movies,
                CurrentPage = 1,
                TotalMovies = 1,
                Sorting = MovieSorting.DateCreated,
                Genre = "action",
                SearchTerm = "lsd"
            };

            var result = moviesController.All(model);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void DetailsShouldReturnView()
        {
            var movie = new Movie
            {
                Title = "TestName",
                ImageUrl = "TestUrl",
                Year = 2000,
                Description = "ugabuga",
                RuntimeInMinutes = 20,
                GenreId = 3,
                Id = 1,
            };

            var data = DatabaseMock.Instance;
            data.Movies.Add(movie);
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var moviesService = new MovieService(data, mapper);

            var moviesController = new MoviesController(moviesService, null, mapper);
            var information = movie.Title + "-" + movie.Year;
            var result = moviesController.Details(movie.Id, information);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void AddGetShouldReturnView()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var movieService = new MovieService(data, mapper);
            var ticketSeller = new TicketSeller
            {
                Id = 1,
                Name = "gosho",
                PhoneNumber = "09999999",
                UserId = "1"
            };

            var ticketSeller1 = new MovieFormModel
            {
                Description = "lesno",
                GenreId = 1,
                ImageUrl = "fakeImageURL",
                RuntimeInMinutes = 0,
                Title = "title",
                Year= 2020

            };
            data.TicketSeller.Add(ticketSeller);
            data.SaveChanges();

            var moviesController = new MoviesController(movieService, null, mapper);
            moviesController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = ClaimsPrincipalMock.Instance()
            };

            var result = moviesController.Add(ticketSeller1);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }


    }
}
