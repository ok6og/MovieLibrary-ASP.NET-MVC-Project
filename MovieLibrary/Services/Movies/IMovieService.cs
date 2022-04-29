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
        MovieDetailsServiceModel Details(int carId);

        int Create(
                string title,
                string description,
                string imageUrl,
                int year,
                int runtimeInMinutes,
                int genreId,
                int ticketSellerId);

        bool Edit(
                int carId,
                string title,
                string description,
                string imageUrl,
                int year,
                int runtimeInMinutes,
                int genreId);
        IEnumerable<MovieServiceModel> ByUser(string userId);

        bool IsByTicketSeller(int movieId, int ticketSellerId);
        IEnumerable<string> AllMovieGenres();
        IEnumerable<MovieGenreServiceModel> AllGenres();

        bool GenreExists(int genreId);
    }
}
