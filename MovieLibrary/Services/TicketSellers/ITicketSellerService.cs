namespace MovieLibrary.Services.TicketSellers
{
    public interface ITicketSellerService
    {
        public bool IsTicketSeller(string userId);
        public int IdByUser(string userId);
    }
}
