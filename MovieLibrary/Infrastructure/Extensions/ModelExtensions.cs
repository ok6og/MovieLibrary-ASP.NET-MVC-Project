using MovieLibrary.Services.Movies;

namespace MovieLibrary.Infrastructure.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IMovieModel movie)
            => movie.Title + "-" + movie.Year;
    }
}
