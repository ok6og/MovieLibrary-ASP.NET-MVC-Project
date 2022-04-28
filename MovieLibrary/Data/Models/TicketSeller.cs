using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants.TicketSeller;

namespace MovieLibrary.Data.Models
{
    public class TicketSeller
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }
        [Required]
        public string UserId { get; set; }
        public IEnumerable<Movie> Movies { get; init; } = new List<Movie>();


    }
}
