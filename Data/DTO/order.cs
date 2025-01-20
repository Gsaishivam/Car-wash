using System.ComponentModel.DataAnnotations;
namespace Car_wash.Data.DTO
{
    public class OrderDTO
    {
        // [Required(ErrorMessage = "User ID is required.")]
        // public int UserID { get; set; }

        [Required(ErrorMessage = "Wash Package Name is required.")]
        [StringLength(100, ErrorMessage = "Wash Package Name cannot exceed 100 characters.")]
        public string WashPackageName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wash Package Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Wash Package Price must be greater than zero.")]
        public decimal WashPackagePrice { get; set; }

        [Required(ErrorMessage = "Order Date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Car Type is required.")]
        [StringLength(50, ErrorMessage = "Car Type cannot exceed 50 characters.")]
        public string CarType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Car Number is required.")]
        [StringLength(20, ErrorMessage = "Car Number cannot exceed 20 characters.")]
        public string CarNumber { get; set; } = string.Empty;
    }
}