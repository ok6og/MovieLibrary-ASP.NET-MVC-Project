using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Data.DataConstants.Genre;

namespace MovieLibrary.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }
        public IEnumerable<Movie> Movies { get; init; } = new List<Movie>();
    }
}
