using Car_wash.Models;
using Car_wash.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Car_wash.Data.DTO;
namespace Car_wash.Repository.Implementation
{
    public class AdminRepo : IAdmin
    {
        private readonly CarWashDBContext _context;
        private readonly IHelper _helperRepo;  // HelperRepo can be injected if necessary

        public AdminRepo(CarWashDBContext context, IHelper helperRepo)
        {
            _context = context;
            _helperRepo = helperRepo;
        }

        public async Task<string> AdminLoginAsync(string email, string password)
        {
            var adminEmail = "saishivam@gmail.com";
            var adminPassword = "12345";
            if (email == adminEmail && password == adminPassword)
            {
                return "Login successful.";
            }
            return "Invalid email or password.";
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int orderID)
        {
            var order = await _context.Orders.FindAsync(orderID);
            if (order == null) 
            {return false;}

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Orders>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }   

        public async Task<Orders> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Washers>> GetWashersAsync()
        {
            return await _context.Washers.ToListAsync();
        }

        public async Task<Washers> GetWasherByIdAsync(int id)
        {
            return await _context.Washers.FindAsync(id);
        }

        public async Task<bool> DeleteWasherAsync(int id)
        {
            var washer = await _context.Washers.FindAsync(id);
            if (washer == null) return false;

            _context.Washers.Remove(washer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<dynamic>> GetLeaderboardAsync()
        {
            var waterSaved = await _context.Washers.OrderByDescending(p => p.Water_saved)
                .Select(w => new { w.WasherID, w.FirstName, w.LastName, w.Water_saved })
                .ToListAsync();

            return waterSaved.Select((w, index) => new
            {
                Rank = index + 1,
                Name = w.FirstName + " " + w.LastName,
                w.WasherID,
                w.Water_saved
            }).ToList();
        }

        public async Task<bool> AddPackageAsync(WashPackages package)
        {
            if (_context.WashPackages.Any(p => p.Name == package.Name && p.Price == package.Price && p.Description == package.Description))
            {
                return false;  // Package already exists
            }

            _context.WashPackages.Add(package);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditPackageAsync(int id, WashPackagesEdit packageEdit)
        {
            var existingPackage = await _context.WashPackages.FindAsync(id);
            if (existingPackage == null) return false;

            if (!string.IsNullOrEmpty(packageEdit.Name))
            {
                // Check if already exists
                if (_context.WashPackages.Any(p => p.Name == packageEdit.Name && p.Price == existingPackage.Price && p.Description == existingPackage.Description))
                {
                    return false;
                }

                existingPackage.Name = packageEdit.Name;
            }

            if (packageEdit.Price.HasValue)
            {
                existingPackage.Price = packageEdit.Price.Value;
            }

            if (!string.IsNullOrEmpty(packageEdit.Description))
            {
                existingPackage.Description = packageEdit.Description;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePackageAsync(int id)
        {
            var package = await _context.WashPackages.FindAsync(id);
            if (package == null) return false;

            _context.WashPackages.Remove(package);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<WashPackages>> GetPackagesAsync()
        {
            return await _context.WashPackages.ToListAsync();
        }
    }
}
