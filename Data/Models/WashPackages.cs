using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Car_wash.Models;

public class WashPackages{
    [Key]
    public int PackageID{get;set;}

    [Required]
    [StringLength(50, ErrorMessage = "Package name can't be longer than 50 characters.")]
    public string Name{get;set;}= null!;

    [Required]
    [StringLength(255, ErrorMessage = "Description can't be longer than 255 characters.")]
    public string Description{get;set;}=null!;

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
    public virtual ICollection<Orders>? Orders { get; set; }
}