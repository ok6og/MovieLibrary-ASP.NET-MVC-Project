using MovieLibrary.Data;

namespace MovieLibrary.Services.TicketSellers
{
    public class TicketSellerService : ITicketSellerService
    {
        private readonly MovieLibraryDbContext data;

        public TicketSellerService(MovieLibraryDbContext data) 
            => this.data = data;

       

        public bool IsTicketSeller(string userId)
            => this.data
                .TicketSeller
                .Any(t => t.UserId == userId);
        public int IdByUser(string userId)
            => this.data
                .TicketSeller
                .Where(t => t.UserId == userId)
                .Select(t => t.Id)
                .FirstOrDefault();
    }
}
