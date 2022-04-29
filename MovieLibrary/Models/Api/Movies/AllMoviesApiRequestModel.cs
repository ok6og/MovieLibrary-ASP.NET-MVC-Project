namespace MovieLibrary.Models.Api.Movies
{
    public class AllMoviesApiRequestModel
    {
        public string Genre { get; init; }
        public string SearchTerm { get; init; }
        public MovieSorting Sorting { get; init; }
        public int CurrentPage { get; init; } = 1;
        public int MoviesPerPage { get; init; } = 10;
    }
}
