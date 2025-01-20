using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Car_wash.Models;

public class Orders{
    [Key]
    public int OrderID { get; set; }
    [ForeignKey("Users")]
    public int UserID { get; set; }
    [ForeignKey("Washers")]
    public int? WasherID { get; set; }

    [ForeignKey("WashPackages")]
    public int PackageID { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Car type cannot exceed 50 characters.")]
    public string CarType { get; set; } 

    [Required]
    [StringLength(20, ErrorMessage = "Car number cannot exceed 20 characters.")]
    public string CarNumber { get; set; }  

    public string? WashPackageName { get; set; }

    public decimal? WashPackagePrice { get; set; }

    [Required(ErrorMessage = "Order date is required.")]
    [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
    public DateTime OrderDate { get; set; }

    public int? OrderStatus { get; set; }  // defaults to 0 (Pending)

    public int? PaymentStatus { get; set; }  // defaults to 0 (Unpaid)

    public int? WashCompletedStatus { get; set; }  // defaults to 0 (Not completed)

    // Navigation properties
    public virtual Users Users { get; set; }=null!;
    public virtual WashPackages WashPackages { get; set; }
    public virtual Washers Washers { get; set; }
    public virtual ICollection<Reviews> Reviews { get; set; }
    }