using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogFront.Models
{
    public class TopicAddModel
    {
        public int AppUserId { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [Display(Name = "Kısa Açıklama")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Resim Seçiniz :")]
        public IFormFile Image { get; set; }
    }
}