using System.ComponentModel.DataAnnotations;

namespace Car_wash.Data.DTO
{
    public class CheckoutDTO{
        [Required]
        public int orderID{set;get;}

        [Required]
        public double amount{set;get;}
    }
}