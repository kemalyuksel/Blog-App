using System.ComponentModel.DataAnnotations;

namespace BlogFront.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage="Bu alan gereklidir.")]
        [Display(Name="Ad")]
        public string Name { get; set; }
    }
}