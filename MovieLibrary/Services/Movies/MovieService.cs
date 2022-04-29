using MovieLibrary.Data;
using MovieLibrary.Models;

namespace MovieLibrary.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly MovieLibraryDbContext data;

        public MovieService(MovieLibraryDbContext data) 
            => this.data = data;

        public MovieQueryServiceModel All(
            string genre,
            string searchTerm,
            MovieSorting sorting,
            int currentPage,
            int moviesPerPage)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(genre))
            {
                moviesQuery = moviesQuery.Where(m => m.Genre.Name == genre);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                moviesQuery = moviesQuery.Where(m =>
                    m.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    m.Year.ToString().ToLower().Contains(searchTerm.ToLower()) ||
                    m.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            moviesQuery = sorting switch
            {

                MovieSorting.Title => moviesQuery.OrderBy(m => m.Title),
                MovieSorting.Year => moviesQuery.OrderByDescending(m => m.Year),
                MovieSorting.Runtime => moviesQuery.OrderBy(m => m.Id),
                MovieSorting.DateCreated or _ => moviesQuery.OrderByDescending(m => m.Id)
            };
            var totalMovies = moviesQuery.Count();

            var movies = moviesQuery
                .Skip((currentPage - 1) * moviesPerPage)
                .Take(moviesPerPage)
                .Select(c => new MovieServiceModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    Genre = c.Genre.Name,
                    ImageUrl = c.ImageUrl,
                    RuntimeInMinutes = c.RuntimeInMinutes,
                    Title = c.Title,
                    Year = c.Year,
                })
                .ToList();

            return new MovieQueryServiceModel
            {
                TotalMovies = totalMovies,
                CurrentPage = currentPage,
                MoviesPerPage = moviesPerPage,
                Movies = movies
            };
        }

        public IEnumerable<string> AllMovieGenres() 
            => this.data
                .Movies
                .Select(m => m.Genre.Name)
                .Distinct()
                .OrderBy(g => g)
                .ToList();
    }
}
