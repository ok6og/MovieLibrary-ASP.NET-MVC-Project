using Microsoft.AspNetCore.Mvc;
using Xunit;
using MovieLibrary.Controllers;
using Moq;
using AutoMapper;
using MovieLibrary.Services.Movies;
using MyShirtsApp.Test.Mocks;
using MovieLibrary.Test.Mocks;
using MovieLibrary.Services.Statistics;
using MovieLibrary.Data.Models;
using System.Collections;
using System.Linq;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using FluentAssertions;
using System;

using static MovieLibrary.Test.Data.Movies;
using static MovieLibrary.WebConstants.Cache;

namespace MyShirtsApp.Test.Controllers
{
    public class HomeControllerTest
    {
        
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;


            var movieService = new MovieService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(movieService,null);

            //act
            var result = homeController.Index();
            //assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;
            var indexViewModel = Assert.IsType<LatestMoviesServiceModel>(model);
        }
        [Fact]
        public void ErrorShouldReturnView()
        {


            var homeController = new HomeController(null, null);

            var result = homeController.Error();

            Assert.NotNull(result);
        }

        [Fact]
        public void IndexShouldReturnView()
        {
            var movieService = new MovieService(DatabaseMock.Instance, MapperMock.Instance);

            var homeController = new HomeController(null,null);

            var result = homeController.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }


    }
}