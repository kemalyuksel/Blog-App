using System.ComponentModel.DataAnnotations;

namespace BlogFront.Models
{
    public class CategoryUpdateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Bu alan boş geçilemez.")]
        [Display(Name="Ad")]
        public string Name { get; set; }
    }
}