using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Car_wash.Models;
using Car_wash.Repository.Interface;
using System.Threading.Tasks;
using Car_wash.Repository.Implementation;

namespace Car_wash.Controllers
{
    [Route("washing")]
    [ApiController]
    public class WashingController : ControllerBase
    {
        private readonly IWashing _washingRepository;
        private readonly GenerateMail _emailService;
        private readonly IHelper _helperRepo;

        public WashingController(IWashing washingRepository, GenerateMail emailService,IHelper helperRepo)   
        {
            _washingRepository = washingRepository;
            _emailService = emailService;
            _helperRepo = helperRepo;
        }

        // Only through washer login
        [Authorize(Roles = "Washer")]
        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _washingRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        // Only through admin login
        [Authorize(Roles = "Admin")]
        [HttpPatch("assign")]
        public async Task<IActionResult> AcceptOrder(int OrderID)
        {
            #region checks
            if (OrderID == 0)
            {
                return BadRequest(new { message = "Order Id cannot be null." });
            }

            var order = await _washingRepository.GetOrderByIdAsync(OrderID);
            if (order == null)
            {
                return BadRequest(new { message = "Order not found." });
            }

            if (order.OrderStatus == 2)
            {
                return BadRequest(new { message = "Order Cancelled" });
            }

            if (order.PaymentStatus != 1)
            {
                return BadRequest(new { message = "Payment pending" });
            }

            if (order.OrderDate < DateTime.Now)
            {
                return BadRequest(new { message = "Wash date already passed" });
            }

            if (order.WashCompletedStatus == 1)
            {
                return BadRequest(new { message = "Wash already completed" });
            }
            #endregion

            #region washers_assign
            try
            {
                await _washingRepository.AssignWasherToOrderAsync(OrderID);
                var orderAfterAssign = await _washingRepository.GetOrderByIdAsync(OrderID);
                var washer = await _helperRepo.GetWasherByOrderIdAsync(OrderID);

                if (washer == null)
                {
                    return BadRequest(new { message = "No washer found for this order." });
                }

                await _emailService.SendEmailAsync(
                    washer.Email,
                    "Welcome to Car Wash Service!",
                    $"Dear {washer.FirstName} {washer.LastName},<br/>You have successfully accepted the order id: {order.OrderID}<br/>"
                );

                return Ok(new { message = "Order accepted by the washer id: " + order.WasherID });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while assigning washer to order.", error = ex.Message });
            }
            #endregion
        }
    }
}
