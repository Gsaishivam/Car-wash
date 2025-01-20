using Car_wash.Data.DTO;
using Car_wash.Models;
using Car_wash.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Car_wash.Repository.Implementation{
    public class LoginRepo : ILogin{
        private readonly CarWashDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly GenerateMail _emailService;

        public LoginRepo(CarWashDBContext context, GenerateMail emailService, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }
        #region Login users/washers
        public async Task<string> Login(Login y)
        {
            var user = await _context.Users.FirstOrDefaultAsync(n => n.Email == y.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(y.Password, user.Password))
            {
                var token = GenerateJwtToken(user.Email, "User", user.UserID);
                await _emailService.SendEmailAsync(
                    y.Email,
                    "Login successful!",
                    $"Dear {user.FirstName} {user.LastName},<br/>You have successfully Logged in<br/>"
                );
                return token;
            }
            // if washer login
                var washer = await _context.Washers.FirstOrDefaultAsync(n => n.Email == y.Email);
                if (washer != null && BCrypt.Net.BCrypt.Verify(y.Password, washer.Password))
                {
                    var token = GenerateJwtToken(washer.Email, "Washer");
                    await _emailService.SendEmailAsync(
                        washer.Email,
                        "Welcome to Car Wash Service!",
                        $"Dear {washer.FirstName} {washer.LastName},<br/>You have successfully Logged in as washer<br/>"
                    );
                    return token;
                }
            return null;
        }

        public async Task<string> Logout()
        {
            return "logout"; 
        }
        #endregion

        #region JWT token
        public string GenerateJwtToken(string email, string role, int userId = 0)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()) // Add user ID claim
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while generating the JWT token.", ex);
            }
        }

        #endregion
        public async Task<Users> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(n => n.Email == email);
        }

        public async Task<Washers> GetWasherByEmail(string email)
        {
            return await _context.Washers.FirstOrDefaultAsync(n => n.Email == email);
        }

}}