using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Infrastructure.Data
{
    public class Movie
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(300)]
        public string Synopsis { get; set; }
        [Required]
        [Column(TypeName="date")]
        public DateTime Release_Date { get; set; }

        [Required]
        [Range(0,500)]
        public int TimeInMinutes { get; set; }

        [Required]
        public double RatingAverage { get; set; }

        [Required]
        public double RatingCount { get; set; }

        [Required]
        public string PosterImageUrl { get; set; }

        [Required]
        public int GenreId { get; set; }
        public MovieGenre Genre { get; init; }

        public ICollection<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();
        public ICollection<MovieWatchlist> MovieWatchList { get; set; } = new List<MovieWatchlist>();


    }
}
