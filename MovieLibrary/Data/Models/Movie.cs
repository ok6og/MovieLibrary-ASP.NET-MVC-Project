using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants;

namespace MovieLibrary.Data.Models
{
    public class Movie
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MovieTitleMaxLength)]
        public string Title { get; set; }   
        
        [Required]
        [MaxLength(MovieDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int RuntimeInMinutes { get; set; }        
        public int GenreId { get; set; }
        public Genre Genre { get; init; }
    }
}
