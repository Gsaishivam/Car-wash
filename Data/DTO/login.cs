using System.ComponentModel.DataAnnotations;
namespace Car_wash.Data.DTO
{
    public class Login
    {
        [Required]
        public string Email { get; set; }=null!;

        [Required]
        public string Password { get; set; }=null!;
    }
}    