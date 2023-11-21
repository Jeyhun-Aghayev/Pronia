using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required,StringLength(10,ErrorMessage ="Uzunluq max 10 olmalidir")]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
    }
}
