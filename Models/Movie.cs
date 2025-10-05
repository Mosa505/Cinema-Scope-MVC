using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_Scope.Models
{
    public class Movie
    {
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Titel { get; set; }
        [Range(1, 10)]
        [Required]

        public double Rate { get; set; }
        [MaxLength(2500)]
        [Required]
        public string StoryLine { get; set; }
        [Required]
        public byte[] Poster { get; set; }
        [Required]
        public int Year { get; set; }

        [ForeignKey("Genre")]
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }


    }
}
