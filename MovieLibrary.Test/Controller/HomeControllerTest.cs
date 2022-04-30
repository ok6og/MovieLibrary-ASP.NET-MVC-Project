using Microsoft.AspNetCore.Mvc;
using Xunit;
using MovieLibrary.Controllers;
using Moq;
using AutoMapper;
using MovieLibrary.Services.Movies;
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
using Microsoft.Extensions.Caching.Memory;

namespace MyShirtsApp.Test.Controllers
{
    public class HomeControllerTest
    {
        
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {

            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var cache = new MemoryCache(new MemoryCacheOptions());

            var movieService = new MovieService(data, mapper);
            var homeController = new HomeController(movieService, cache);


            var result = homeController.Index();
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);


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
            var cache = new MemoryCache(new MemoryCacheOptions());
            var movieService = new MovieService(DatabaseMock.Instance, MapperMock.Instance);

            var homeController = new HomeController(movieService, cache);

            var result = homeController.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }


    }
}