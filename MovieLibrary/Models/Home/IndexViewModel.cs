namespace MovieLibrary.Models.Home
{
    public class IndexViewModel
    {
        public int TotalMovies { get; init; }
        public int TotalUsers { get; init; }
        public int TotalWatches { get; init; }
        public List<MovieIndexViewModel> Movies { get; init; }
    }
}
