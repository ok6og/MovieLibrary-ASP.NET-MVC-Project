using MovieLibrary.Models;

namespace MovieLibrary.Services.Movies
{
    public interface IMovieService
    {
        MovieQueryServiceModel All(
            string genre,
            string searchTerm,
            MovieSorting sorting,
            int currentPage,
            int moviesPerPage);

        IEnumerable<string> AllMovieGenres();
    }
}
