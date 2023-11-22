using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required,StringLength(25,ErrorMessage ="Uzunluq max 10 olmalidir")]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        [StringLength(maximumLength: 100)]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
