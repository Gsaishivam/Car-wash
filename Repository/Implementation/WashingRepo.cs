using Microsoft.EntityFrameworkCore;
using Car_wash.Models;
using Car_wash.Repository.Interface;
using Car_wash.Repository.Implementation;

namespace Car_wash.Repository.Implementation
{
    public class WashingRepo : IWashing
    {
        private readonly CarWashDBContext _context;
        private readonly IHelper _helperRepo;
        
        public WashingRepo(CarWashDBContext context, IHelper helperRepo)
        {
            _context = context;
            _helperRepo = helperRepo;
        }

        public async Task<List<Orders>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Orders> GetOrderByIdAsync(int orderId)
        {
            return await _helperRepo.GetOrderByIdAsync(orderId);
        }

        public async Task AssignWasherToOrderAsync(int orderId)
        {
            var order = await _helperRepo.GetOrderByIdAsync(orderId);
            if (order == null) return;

            // Check if there are any available washers
            var availableWashers = await _context.Washers.Where(w => !w.Orders.Any()).ToListAsync();

            if (availableWashers.Any())
            {
                var availableWasher = availableWashers.First();
                order.WasherID = availableWasher.WasherID;
                availableWasher.Orders.Add(orderId);
            }
            else
            {
                var allWashers = await _context.Washers.ToListAsync();
                var randomWasher = allWashers.OrderBy(r => Guid.NewGuid()).First(); // Assign random washer
                order.WasherID = randomWasher.WasherID;
                randomWasher.Orders.Add(orderId);
            }

            // Update the washer status
            var washer = await _helperRepo.GetWasherByOrderIdAsync(orderId);
            if (washer != null)
            {
                washer.LastLogin = DateTime.Now;
                washer.IsActive = true;
                var random = new Random();
                washer.Water_saved = random.Next(50, 150); 
            }

            order.WashCompletedStatus = 1; 
            await _helperRepo.SaveChangesAsync();
        }
    }
}
