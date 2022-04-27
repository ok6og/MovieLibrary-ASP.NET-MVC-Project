using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants;

namespace MovieLibrary.Models.Movies
{
    public class AddMovieFormModel
    {

        [Required]
        [StringLength(MovieTitleMaxLength, MinimumLength = MovieTitleMinLength)]       
        public string Title { get; init; }

        [Required]
        [StringLength(MovieDescriptionMaxLength, MinimumLength = MovieTitleMinLength)]
        public string Description { get; init; }

        [StringLength(ImageUrlMaxLength)]
        [Display(Name ="Image URL")]
        [Required]
        [Url]                
        public string ImageUrl { get; init; }

        [Range(MovieYearMinValue,MovieYearMaxValue)]

        public int Year { get; init; }

        [Display(Name = "Runtime In Minutes")]
        [Range(MovieRuntimeMinValue,MovieRuntimeMaxValue)]
        public int RuntimeInMinutes { get; init; }

        [Display(Name = "Genre")]
        public int GenreId { get; init; }
        public IEnumerable<MovieGenreViewModel>? Genres { get; set; }
    }
}
