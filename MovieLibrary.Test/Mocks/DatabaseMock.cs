using System;
using MovieLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace MyShirtsApp.Test.Mocks
{
    
    public static class DatabaseMock
    {
        public static MovieLibraryDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<MovieLibraryDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new MovieLibraryDbContext(dbContextOptions);
            }
        }
    }
}