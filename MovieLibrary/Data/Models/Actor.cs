using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants.ImageUrl;
using static MovieLibrary.Data.DataConstants.Actor;
namespace MovieLibrary.Data.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; init; }
        [MaxLength(UrlMaxLength)]
        [Display(Name = "Profile Picture URL")]
        public string ProfilePicture { get; set; }

        [MaxLength(FullNameMaxValue)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [MaxLength(BioMaxLength)]
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        public List<ActorMovie> ActorsMovies { get; set; }

    }
}
