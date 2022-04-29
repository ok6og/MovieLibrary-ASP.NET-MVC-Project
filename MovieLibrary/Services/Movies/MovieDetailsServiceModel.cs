namespace MovieLibrary.Services.Movies
{
    public class MovieDetailsServiceModel : MovieServiceModel
    {
        public string Description { get; init; }
        public int TicketSellerId { get; init; }
        public string TicketSellerName { get; init; }
        public string GenreName { get; init; }
        public int GenreId { get; init; }
        public string UserId { get; init; }
    }
}
