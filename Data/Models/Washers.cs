using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Car_wash.Models;

public class Washers{
    [Key]
    public int WasherID{get;set;}

    [ForeignKey("Roles")]
    public int? RoleID{get;set;}

    [ForeignKey("Orders")]
    public List<int> Orders { get; set; } = new List<int>();

    [Required]
    [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
    public string FirstName{get;set;}= null!;

    [Required]
    [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
    public string LastName{get;set;}=null!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be at least 3 characters long.")]
    public string Password { get; set; } = null!;

    public string? ProfilePicture_Url{get;set;}

    [Required]
    public bool IsActive{get;set;}=false;

    public DateTime LastLogin{get;set;}=DateTime.Now;

    [Required]
    public int Water_saved{get;set;}=0;

    public virtual Roles Roles { get; set; }

    //public virtual ICollection<Orders> Orders { get; set; }
    // public virtual Orders? CurrentOrder { get; set; }
}