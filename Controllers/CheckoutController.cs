using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Car_wash.Data.DTO;
using Car_wash.Repository.Interface; 


namespace Car_wash.Controllers
{
    [Route("checkout")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CheckoutController : ControllerBase
    {
        private readonly GenerateMail _emailService;
        private readonly ICheckout _checkoutRepo;  
        private readonly IHelper _helperRepo;      
        public CheckoutController(ICheckout checkoutRepo, IHelper helperRepo, GenerateMail emailService)
        {
            _checkoutRepo = checkoutRepo;
            _helperRepo = helperRepo;
            _emailService = emailService;
        }

        #region Checkout
        [HttpPatch]
        public async Task<IActionResult> Checkout(CheckoutDTO checkoutDTO)
        {
            try
            {
                if (checkoutDTO.orderID == 0)
                {
                    return BadRequest(new { message = "Order Id cannot be null." });
                }

                // Use the repository to fetch the order
                var order = await _checkoutRepo.GetOrderByIdAsync(checkoutDTO.orderID);
                if (order == null)
                {
                    return BadRequest(new { message = "Order not found." });
                }
                if(order.PaymentStatus==1){
                    return BadRequest(new{message="Already checked out."});
                }
                if (order.OrderStatus != 1)
                {
                    return BadRequest(new { message = "Order is not accepted." });
                }

                if (checkoutDTO.amount < (double?)order.WashPackagePrice)
                {
                    return BadRequest(new { message = "Insufficient amount." });
                }

                // Process checkout using the repository
                var success = await _checkoutRepo.ProcessCheckoutAsync(checkoutDTO);
                if (!success)
                {
                    return BadRequest(new { message = "Checkout processing failed." });
                }

                // Get user details using HelperRepo
                var user = await _helperRepo.GetUserByOrderIdAsync(order.OrderID);
                if (user == null)
                {
                    return BadRequest(new { message = "User not found." });
                }

                // Send email to user
                await _emailService.SendEmailAsync(
                    user.Email,
                    "Order Checkout",
                    $"Your order has been checked out. Total amount: {checkoutDTO.amount} and the remaining amount: {checkoutDTO.amount - (double?)order.WashPackagePrice}"
                );

                return Ok(new { message = "Checked out successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred: " + ex.Message });
            }
        }
        #endregion
    }
}
