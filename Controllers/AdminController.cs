using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Car_wash.Models;
using Car_wash.Repository.Interface;
using Car_wash.Data.DTO;
using Car_wash.Repository.Implementation;

namespace Car_wash.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _adminRepo;
        private readonly IHelper _helperRepo;
        private readonly ILogin _loginRepo; // for jwt token gen only

        public AdminController(IAdmin adminRepo, IHelper helperRepo, ILogin loginRepo)
        {
            _adminRepo = adminRepo;
            _helperRepo = helperRepo;
            _loginRepo = loginRepo;
        }

        #region Login
        private string adminEmail = "saishivam@gmail.com";
        private string adminPassword = "12345";
        
        [HttpPost("login")]
        public async Task<IActionResult> AdminLogin(Admin x)
        {
            try
            {
                if (x.Email == null || x.Password == null)
                {
                    return BadRequest(new { message = "Please provide email and password to log in." });
                }

                if (adminEmail != null && adminPassword != null && x.Email == adminEmail && x.Password == adminPassword)
                {
                    var token = _loginRepo.GenerateJwtToken(adminEmail, "Admin",0);
                    return Ok(new { message = "Login successful.", token });
                }

                return Unauthorized(new { message = "Invalid email or password." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "You have been logged out successfully." });
        }
        #endregion

        #region Users
        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            try
            {
                var users = await _adminRepo.GetUsersAsync();
                if (users == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching usershvhgcgfxc: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users/{id}")]
        public async Task<ActionResult<Users>> GetUsersById(int id)
        {
            try
            {
                var user = await _adminRepo.GetUserByIdAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching user: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _adminRepo.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "User not found." });
                }

                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting user: " + ex.Message });
            }
        }
        #endregion

        #region Orders
        [Authorize(Roles = "Admin")]
        [HttpDelete("orders/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var result = await _adminRepo.DeleteOrderAsync(orderId);
                if (!result)
                {
                    return NotFound(new { message = "Order not found." });
                }

                return Ok(new { message = "Order deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting order: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders() 
        {
            try
            {
                var orders = await _adminRepo.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching orders: " + ex.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("orders/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                var order = await _adminRepo.GetOrderByIdAsync(orderId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching order: " + ex.Message });
            }
        }
        #endregion

        #region Washers
        [Authorize(Roles = "Admin")]
        [HttpGet("washers")]
        public async Task<ActionResult<IEnumerable<Washers>>> GetWashers()
        {
            try
            {
                var washers = await _adminRepo.GetWashersAsync();
                return Ok(washers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching washers: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("washers/{id}")]
        public async Task<ActionResult<Washers>> GetWasher(int id)
        {
            try
            {
                var washer = await _adminRepo.GetWasherByIdAsync(id);
                if (washer == null)
                {
                    return NotFound(new { message = "Washer not found." });
                }

                return Ok(washer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching washer: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("washers/{id}")]
        public async Task<IActionResult> DeleteWasher(int id)
        {
            try
            {
                var result = await _adminRepo.DeleteWasherAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Washer not found." });
                }

                return Ok(new { message = "Washer deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting washer: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            try
            {
                var leaderboard = await _adminRepo.GetLeaderboardAsync();
                return Ok(leaderboard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching leaderboard: " + ex.Message });
            }
        }
        #endregion

        #region Packages
        [Authorize(Roles = "Admin")]
        [HttpPost("packages")]
        public async Task<IActionResult> AddPackage([FromBody] WashPackages x)
        {
            try
            {
                var result = await _adminRepo.AddPackageAsync(x);
                if (!result)
                {
                    return BadRequest(new { message = "Package already exists." });
                }

                return Ok(new { message = "Package added successfully.", packageID = x.PackageID });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error adding package: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("packages/{id}")]
        public async Task<IActionResult> EditPackage(int id, [FromBody] WashPackagesEdit x)
        {
            try
            {
                var result = await _adminRepo.EditPackageAsync(id, x);
                if (!result)
                {
                    return NotFound(new { message = "Package not found." });
                }

                return Ok(new { message = "Package updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating package: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("packages/{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            try
            {
                var result = await _adminRepo.DeletePackageAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Package not found." });
                }

                return Ok(new { message = "Package deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting package: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("packages")]
        public async Task<IActionResult> GetPackages()
        {
            try
            {
                var packages = await _adminRepo.GetPackagesAsync();
                return Ok(packages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching packages: " + ex.Message });
            }
        }
        #endregion
    }
}

public class Admin{
    public string Email { get; set; }
    public string Password { get; set; }
}