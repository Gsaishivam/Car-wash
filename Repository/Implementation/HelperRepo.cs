using Car_wash.Models;
using Car_wash.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_wash.Repository.Implementation
{
    public class HelperRepo:IHelper
    {
        private readonly CarWashDBContext _context;
        public HelperRepo(CarWashDBContext context)
        {
            _context = context;
        }
        public async Task<Orders> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

        public async Task<Users> GetUserByOrderIdAsync(int orderId)
        {
            var order = await _context.Orders.Include(o => o.Users).FirstOrDefaultAsync(o => o.OrderID == orderId);
            return order?.Users;
        }

        public async Task<Washers> GetWasherByOrderIdAsync(int orderId)
        {
            var order = await _context.Orders.Include(o => o.Washers).FirstOrDefaultAsync(o => o.OrderID == orderId);
            return order?.Washers;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
