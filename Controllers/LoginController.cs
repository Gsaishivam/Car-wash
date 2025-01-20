using Microsoft.AspNetCore.Mvc;
//using Car_wash.Repository.Implementation;
using Car_wash.Data.DTO;
//using Microsoft.AspNetCore.Authorization;
using Car_wash.Repository.Interface;

namespace Car_wash.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin loginRepo;

        public LoginController(ILogin loginRepo)
        {
            this.loginRepo = loginRepo;
        }
        #region Login users/washers
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login y)
        {
            try
            {
                if (y.Email == null || y.Password == null)
                {
                    return BadRequest(new { message = "Please provide email and password to log in." });
                }

                // Use LoginRepo for login process
                var token = await loginRepo.Login(y);

                if (token == null)
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }

                // Determine if user or washer is logged in by checking token response
                var user = await loginRepo.GetUserByEmail(y.Email);
                var washer = await loginRepo.GetWasherByEmail(y.Email);

                var response = new
                {
                    message = "Login successful.",
                    token = token,
                    userId = user?.UserID,
                    washerId = washer?.WasherID
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login: " + ex.Message });
            }
        }
        #endregion

        #region Logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                return Ok(new { message = "You have been logged out successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during logout: " + ex.Message });
            }
        }
        #endregion
    }
        
        
}
