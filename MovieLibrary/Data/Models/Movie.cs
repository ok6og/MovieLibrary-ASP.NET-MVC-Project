using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants.Movie;
using static MovieLibrary.Data.DataConstants.ImageUrl;

namespace MovieLibrary.Data.Models
{
    public class Movie
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }   
        
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int RuntimeInMinutes { get; set; }        
        public int GenreId { get; set; }
        public Genre Genre { get; init; }
        public int TicketSellerId { get; init; }
        public TicketSeller TicketSeller { get; init; }
    }
}
