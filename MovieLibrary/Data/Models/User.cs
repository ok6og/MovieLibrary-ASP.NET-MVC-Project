using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants.User;

namespace MovieLibrary.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

    }
}
