using MovieLibrary.Models.Api.Movies;

namespace MovieLibrary.Services.Movies
{
    public class MovieQueryServiceModel
    {
        public int CurrentPage { get; init; }
        public int MoviesPerPage { get; init; }
        public int TotalMovies { get; init; }
        public IEnumerable<MovieServiceModel> Movies { get; init; }
    }
}
