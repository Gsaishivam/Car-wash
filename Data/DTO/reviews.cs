using System.ComponentModel.DataAnnotations;
namespace Car_wash.Data.DTO
{
    public class ReviewsDTO
    {
        [Required]
        public int OrderID { get; set; }
        [Required]
        public float Rating { get; set; }
        [Required]
        public string Review_comment { get; set; }=null!;
    }
}