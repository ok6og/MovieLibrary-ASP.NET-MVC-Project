using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieLibrary.Test.Data
{
    public static class Movies
    {
        public static IEnumerable<Movie> TenMovies
           => Enumerable.Range(0, 10).Select(i => new Movie
           {
               IsPublic = true,
           });
    }
}
