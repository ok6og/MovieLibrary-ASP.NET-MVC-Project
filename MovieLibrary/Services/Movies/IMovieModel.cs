namespace MovieLibrary.Services.Movies
{
    public interface IMovieModel
    {
        string Title { get; }
        string GenreName { get; }
        int Year { get; }
    }
}
