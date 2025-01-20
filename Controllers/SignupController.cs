using Car_wash.Data.DTO;
using Car_wash.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Car_wash.Controllers
{
    [Route("signup")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ISignup _signupRepo;  // Inject the Signup repository

        public SignupController(ISignup signupRepo)
        {
            _signupRepo = signupRepo;
        }

        #region Washer_signup
        [HttpPost("washer")]
        public async Task<IActionResult> Washer_Signup(Signup x)
        {
            // Call the repository to handle signup logic
            var result = await _signupRepo.Washer_Signup(x);

            if (result == "Washer registered successfully.")
            {
                return Ok(new { message = result });
            }
            else if (result == "Washer already exists.")
            {
                return BadRequest(new { message = result });
            }
            else
            {
                return StatusCode(500, new { message = result }); 
            }
        }
        #endregion

        #region User_signup
        [HttpPost("user")]
        public async Task<IActionResult> User_Signup(Signup y)
        {
            // Call the repository to handle signup logic
            var result = await _signupRepo.User_Signup(y);

            if (result == "User registered successfully.")
            {
                return Ok(new { message = result });
            }
            else if (result == "User already exists.")
            {
                return BadRequest(new { message = result });
            }
            else
            {
                return StatusCode(500, new { message = result }); 
            }
        }
        #endregion
    }
}
