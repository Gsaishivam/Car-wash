using Car_wash.Data.DTO;
using Car_wash.Models;
using Car_wash.Repository.Interface;
using AutoMapper;

namespace Car_wash.Repository.Implementation{
    public class SignupRepo:ISignup {
        private readonly CarWashDBContext _context;
        private readonly GenerateMail _emailService;
        private readonly IMapper _mapper;

        public SignupRepo(CarWashDBContext context, GenerateMail emailService, IMapper mapper)
        {
            _context = context;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<string> Washer_Signup(Signup x)
        {
            // Check if washer already exists 
            if (_context.Washers.Any(w => w.Email == x.Email || w.PhoneNumber == x.PhoneNumber))
            {
                return "Washer already exists.";
            }
            try
            {
                // Map DTO to Washer entity
                var washer = _mapper.Map<Washers>(x);

                _context.Washers.Add(washer);
                await _context.SaveChangesAsync();
                await _emailService.SendEmailAsync(
                    x.Email,
                    "Welcome to Car Wash Service!",
                    $"Dear {x.FirstName} {x.LastName},<br/>You have successfully registered as a washer in our system.<br/>Thank you for joining us!"
                );

                return "Washer registered successfully.";
            }
            catch (Exception ex)
            {
                return $"An unexpected error occurred: {ex.Message}";
            }
        }

        public async Task<string> User_Signup(Signup y)
        {
            // Check if user already exists
            if (_context.Users.Any(u => u.Email == y.Email || u.PhoneNumber == y.PhoneNumber))
            {
                return "User already exists.";
            }
            try
            {
                // Map DTO to User entity
                var user = _mapper.Map<Users>(y);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                await _emailService.SendEmailAsync(
                    y.Email,
                    "Welcome to Car Wash Service!",
                    $"Dear {y.FirstName} {y.LastName},<br/>You have successfully registered as a user in our system.<br/>Thank you for joining us!"
                );
                return "User registered successfully.";
            }
            catch (Exception ex)
            {
                return $"An unexpected error occurred: {ex.Message}";
            }
        }
    }
}