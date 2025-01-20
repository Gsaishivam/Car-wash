using System.ComponentModel.DataAnnotations;
namespace Car_wash.Models;

public class Roles{
    [Key]
    [Range(0,2, ErrorMessage = "RoleId must be between 0 and 2")]
    public int RoleID{get;set;}

    [Required]
    [StringLength(50, ErrorMessage = "Role name can't be longer than 50 characters.")]
    public string RoleName{get;set;}

    public virtual ICollection<Users> Users { get; set; }
    public virtual ICollection<Washers> Washers { get; set; }

}