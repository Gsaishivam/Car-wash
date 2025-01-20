using System.ComponentModel.DataAnnotations;
namespace Car_wash.Data.DTO
{    
    public class Signup
    {
    [Required]
    [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
    public string FirstName{get;set;}= null!;

    [Required]
    [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
    public string LastName{get;set;}=null!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = null!;
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }=null!;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public string Password { get; set; } = null!;

    }
}
