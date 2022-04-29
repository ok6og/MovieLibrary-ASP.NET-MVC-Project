using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants.Movie;
using static MovieLibrary.Data.DataConstants.ImageUrl;
using MovieLibrary.Services.Movies;

namespace MovieLibrary.Models.Movies
{
    public class MovieFormModel
    {

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]       
        public string Title { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = TitleMinLength)]
        public string Description { get; init; }

        [StringLength(UrlMaxLength)]
        [Display(Name ="Image URL")]
        [Required]
        [Url]                
        public string ImageUrl { get; init; }

        [Range(YearMinValue,YearMaxValue)]

        public int Year { get; init; }

        [Display(Name = "Runtime In Minutes")]
        [Range(RuntimeMinValue,RuntimeMaxValue)]
        public int RuntimeInMinutes { get; init; }

        [Display(Name = "Genre")]
        public int GenreId { get; init; }
        public IEnumerable<MovieGenreServiceModel>? Genres { get; set; }
    }
}
