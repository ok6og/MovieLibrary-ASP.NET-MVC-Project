using AutoMapper;
using AutoMapper.QueryableExtensions;
using MovieLibrary.Data;
using MovieLibrary.Data.Models;
using MovieLibrary.Models;

namespace MovieLibrary.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly MovieLibraryDbContext data;
        private readonly IMapper mapper;

        public MovieService(MovieLibraryDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }
         

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

            var movies = GetMovies(moviesQuery
                .Skip((currentPage - 1) * moviesPerPage)
                .Take(moviesPerPage));



            return new MovieQueryServiceModel
            {
                TotalMovies = totalMovies,
                CurrentPage = currentPage,
                MoviesPerPage = moviesPerPage,
                Movies = movies
            };
        }

        public MovieDetailsServiceModel Details(int id)
        {                           
            var movie = this.data
            .Movies
            .Where(m => m.Id == id)
            .ProjectTo<MovieDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefault();

            return movie;
        }
            

        public IEnumerable<MovieServiceModel> ByUser(string userId)
            => GetMovies(this.data
                .Movies
                .Where(m => m.TicketSeller.UserId == userId));

        public IEnumerable<string> AllMovieGenres()
            => this.data
                .Movies
                .Select(m => m.Genre.Name)
                .Distinct()
                .OrderBy(g => g)
                .ToList();

        public IEnumerable<MovieGenreServiceModel> AllGenres()
            => this.data
                .Genres
                .Select(x => new MovieGenreServiceModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();



        private static IEnumerable<MovieServiceModel> GetMovies(IQueryable<Movie> movieQuery)
            => movieQuery
                .Select(c => new MovieServiceModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    GenreName = c.Genre.Name,
                    ImageUrl = c.ImageUrl,
                    RuntimeInMinutes = c.RuntimeInMinutes,
                    Title = c.Title,
                    Year = c.Year,
                })
                    .ToList();

        public bool GenreExists(int genreId)
            => this.data.Genres.Any(g => g.Id == genreId);

        public int Create(string title, string description, string imageUrl, int year, int runtimeInMinutes, int genreId, int ticketSellerId)
        {
            var movieData = new Movie
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                Year = year,
                RuntimeInMinutes = runtimeInMinutes,
                GenreId = genreId,
                TicketSellerId = ticketSellerId
            };

            this.data.Movies.Add(movieData);
            this.data.SaveChanges();

            return movieData.Id;
        }

        public bool Edit(int id, string title, string description, string imageUrl, int year, int runtimeInMinutes, int genreId)
        {
            var movieData = this.data.Movies.Find(id);

            if (movieData == null)
            {
                return false;
            }

            movieData.Title = title;
            movieData.Description = description;
            movieData.ImageUrl = imageUrl;
            movieData.Year = year;
            movieData.RuntimeInMinutes = runtimeInMinutes;
            movieData.GenreId = genreId;



            this.data.SaveChanges();

            return true;
        }

        public bool IsByTicketSeller(int movieId, int ticketSellerId)
            => this.data
            .Movies
            .Any(m => m.Id == movieId && m.TicketSellerId == ticketSellerId);

        public IEnumerable<LatestMoviesServiceModel> Latest()
            => this.data
                .Movies
                .OrderByDescending(m => m.Id)
                .ProjectTo<LatestMoviesServiceModel>(this.mapper.ConfigurationProvider)
                .Take(3)
                .ToList();
    }
}
