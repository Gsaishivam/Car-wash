using Microsoft.AspNetCore.Mvc;
using Car_wash.Data.DTO;
using Car_wash.Repository.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Car_wash.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _orderRepository;
        private readonly GenerateMail _emailService;  // Email service injected here

        public OrdersController(IOrder orderRepository, GenerateMail emailService)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
        }

        #region Order Booking
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderDTO orderRequest)
        {
            try
            {
                // Extract UserID from JWT token
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

                if (userID == 0)
                {
                    return Unauthorized(new { message = "User not authorized." });
                }

                // Pass the ClaimsPrincipal directly (no need to pass UserID)
                var order = await _orderRepository.AddOrderAsync(orderRequest, User); // Pass ClaimsPrincipal (User) instead of UserID
                if (order == null)
                {
                    return BadRequest(new { message = "Invalid user or wash package." });
                }

                // Fetch the user to send the email notification
                var user = await _orderRepository.GetUsersByIdAsync(userID);
                if (user == null)
                {
                    return BadRequest(new { message = "User not found." });
                }

                // Send email notification to the user after a successful order creation
                await _emailService.SendEmailAsync(
                    user.Email,
                    "Order Placed!",
                    $"Dear {user.FirstName} {user.LastName},<br/>You have successfully placed an order with order id {order.OrderID}.<br/>Thank you for joining us!<br/>"
                );

                return Ok(new { message = "Order added successfully", orderID = order.OrderID });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred: " + ex.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateOrder(int orderID, OrderDTO orderRequest)
        {
            try
            {
                var order = await _orderRepository.UpdateOrderAsync(orderID, orderRequest);
                if (order == null)
                {
                    return BadRequest(new { message = "Order not found or invalid update." });
                }

                // Send email notification about order update
                var user = await _orderRepository.GetUsersByIdAsync(order.UserID);
                if (user == null)
                {
                    return BadRequest(new { message = "User not found." });
                }

                await _emailService.SendEmailAsync(
                    user.Email,
                    "Order Updated!",
                    $"Dear {user.FirstName} {user.LastName},<br/>Your order with Order ID {order.OrderID} has been updated.<br/>Thank you for using our service!"
                );

                return Ok(new { message = "Order updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating order: " + ex.Message });
            }
        }
        #endregion
    }
}
