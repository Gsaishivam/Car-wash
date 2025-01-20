using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Car_wash.Models;

public class Users{
    [Key]
    public int UserID{get;set;}

    [ForeignKey("Roles")]
    public int RoleID{get;set;}

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

    public string? ProfilePicture_Url{get;set;}

    [Required]
    public bool IsActive{get;set;}=false;

    public DateTime LastLogin{get;set;}=DateTime.Now;

    public virtual Roles Roles{get;set;}
    public virtual ICollection<Orders> Orders { get; set; }
}