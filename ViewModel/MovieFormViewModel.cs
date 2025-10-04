using Cinema_Scope.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_Scope.ViewModel
{
    public class MovieFormViewModel
    {
        [Required]
        [StringLength(50)]
        public string Titel { get; set; }
        [Range(1, 5)]
        [Required]
        public double Rate { get; set; }
        [StringLength(2500)]
        [Required]
        public string StoryLine { get; set; }

        [Display(Name ="Select Poster...")]
        public byte[] Poster { get; set; }
        [Required]
        public int Year { get; set; }

        [Display(Name ="Genre")]
        public byte GenreId { get; set; }

        public List<Genre> Genres { get; set; } 

    }
}
