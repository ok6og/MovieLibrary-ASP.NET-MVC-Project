using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants.TicketSeller;
namespace MovieLibrary.Models.TicketSeller
{
    public class BecomeTicketSellerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
