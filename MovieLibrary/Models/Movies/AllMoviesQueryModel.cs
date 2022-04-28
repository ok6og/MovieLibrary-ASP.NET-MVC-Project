using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models.Movies
{
    public class AllMoviesQueryModel
    {
        public const int MoviesPerPage = 3;
        public string Genre { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }
        public MovieSorting Sorting { get; init; }
        public int CurrentPage { get; init; } = 1;
        public int TotalMovies { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<MovieListingViewModel> Movies { get; set; }
    }
}
