using System.ComponentModel.DataAnnotations;

namespace BlogFront.Models
{
    public class CommentApprovedModel
    {
        public int Id { get; set; }
        [Display(Name="Yorum")]
        public string Description { get; set; }
        [Display(Name="Yazar")]
        public string AuthorName { get; set; }
        [Display(Name="Yazar Mail")]
        public string AuthorEmail { get; set; }
        [Display(Name="Onay")]
        public bool IsApproved { get; set; } 
    }
}