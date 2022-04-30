using MovieLibrary.Models;

namespace MovieLibrary.Services.Movies
{
    public interface IMovieService
    {
        MovieQueryServiceModel All(
            string genre = null,
            string searchTerm = null,
            MovieSorting sorting = MovieSorting.DateCreated,
            int currentPage = 1,
            int moviesPerPage = int.MaxValue,
            bool publicOnly = true);
        MovieDetailsServiceModel Details(int carId);

        public IEnumerable<LatestMoviesServiceModel>Latest();

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
                int genreId,
                bool IsPublic);
        IEnumerable<MovieServiceModel> ByUser(string userId);

        bool IsByTicketSeller(int movieId, int ticketSellerId);

        void ChangeVisibility(int movieId);
        IEnumerable<string> AllMovieGenres();
        IEnumerable<MovieGenreServiceModel> AllGenres();

        bool GenreExists(int genreId);

        public bool Delete(int id);
    }
}
