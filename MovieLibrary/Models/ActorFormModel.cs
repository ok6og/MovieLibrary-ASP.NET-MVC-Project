using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants.ImageUrl;
using static MovieLibrary.Data.DataConstants.Actor;

namespace MovieLibrary.Models
{
    public class ActorFormModel
    {
        [MaxLength(UrlMaxLength)]
        [Display(Name = "Profile Picture URL")]
        [Required]
        public string ProfilePicture { get; set; }

        [StringLength(FullNameMaxValue, MinimumLength =FullNameMinValue)]
        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }

        [StringLength(BioMaxLength, MinimumLength =BioMinLength)]
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        public int Id { get; init; }
    }
}
