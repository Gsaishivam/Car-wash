using Car_wash.Repository.Interface;
using Car_wash.Models;
using Microsoft.EntityFrameworkCore;
using Car_wash.Data.DTO;

namespace Car_wash.Repository.Implementation
{
    public class CheckoutRepo : ICheckout
    {
        private readonly CarWashDBContext _context;

        public CheckoutRepo(CarWashDBContext context)
        {
            _context = context;
        }
        public async Task<bool> ProcessCheckoutAsync(CheckoutDTO checkout)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(p => p.OrderID == checkout.orderID);
            if (order == null)
            {
                return false; 
            }
            if (order.OrderStatus != 1)
            {
                return false; 
            }
            if (checkout.amount < (double)order.WashPackagePrice)
            {
                return false; 
            }
            order.PaymentStatus = 1; 
            await _context.SaveChangesAsync();
            return true; 
        }
        public async Task<Orders> GetOrderByIdAsync(int orderID)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderID);
        }
        public async Task<Users> GetUserByIdAsync(int userID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserID == userID);
        }
    }
}
