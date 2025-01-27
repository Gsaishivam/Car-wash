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
            Console.WriteLine($"Available washers count: {availableWashers.Count}");
            if (availableWashers.Any())
            {
                var availableWasher = availableWashers.First();
                order.WasherID = availableWasher.WasherID;
                availableWasher.Orders.Add(orderId);
                Console.WriteLine($"Assigning available washer: {availableWasher.FirstName} {availableWasher.LastName}, OrderID: {order.OrderID}");
            }
            else
            {
                var allWashers = await _context.Washers.ToListAsync();
                Console.WriteLine($"Total washers count: {allWashers.Count}");
                foreach (var wa in allWashers)
        {
            Console.WriteLine($"Washer ID: {wa.WasherID}, Orders Count: {wa.Orders.Count}");
        }
                var randomWasher = allWashers.OrderBy(r => Guid.NewGuid()).First(); // Assign random washer
                Console.WriteLine($"Assigning random washer: {randomWasher.FirstName} {randomWasher.LastName}, OrderID: {order.OrderID}");
                order.WasherID = randomWasher.WasherID;
                randomWasher.Orders.Add(orderId);
            }

            if (order.WasherID.HasValue) // Check if a washer was assigned
            {
                var washer = await _context.Washers.FindAsync(order.WasherID.Value); // Retrieve washer directly by ID

                if (washer != null)
                {
                    washer.LastLogin = DateTime.Now;
                    washer.IsActive = true;
                    var random = new Random();
                    washer.Water_saved += random.Next(50, 150); 

                    _context.Washers.Update(washer); //Explicitly update the Washer entity
                }
            }

            order.WashCompletedStatus = 1;
            _context.Orders.Update(order); //Explicitly update the Order entity
            await _context.SaveChangesAsync();}

            }
}
