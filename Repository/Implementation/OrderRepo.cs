using Microsoft.EntityFrameworkCore;
using Car_wash.Models;
using Car_wash.Data.DTO;
using Car_wash.Repository.Interface;
using System.Security.Claims;

namespace Car_wash.Repository.Implementation
{
    public class OrderRepo : IOrder
    {
        private readonly CarWashDBContext _context;

        public OrderRepo(CarWashDBContext context)
        {
            _context = context;
        }

        public async Task<Orders> AddOrderAsync(OrderDTO orderRequest, ClaimsPrincipal userClaims)
        {
            // Extract UserID from ClaimsPrincipal (JWT token)
            var userID = int.Parse(userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Make sure the UserID exists
            var user = await _context.Users.FindAsync(userID);
            if (user == null) return null;

            var package = await _context.WashPackages
                .Where(p => p.Name == orderRequest.WashPackageName && p.Price == orderRequest.WashPackagePrice)
                .FirstOrDefaultAsync();
            if (package == null) return null;

            var order = new Orders
            {
                UserID = user.UserID,  // Use the UserID extracted from the JWT token
                PackageID = package.PackageID,
                OrderDate = orderRequest.OrderDate,
                OrderStatus = 1, // Active/Booked
                PaymentStatus = 0, // Unpaid
                CarType = orderRequest.CarType,
                CarNumber = orderRequest.CarNumber,
                WashPackageName = orderRequest.WashPackageName,
                WashPackagePrice = orderRequest.WashPackagePrice
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }



        public async Task<Orders> UpdateOrderAsync(int orderID, OrderDTO orderRequest)
        {
            var order = await _context.Orders.FindAsync(orderID);
            if (order == null) return null;

            var package = await _context.WashPackages
                .Where(p => p.Name == orderRequest.WashPackageName && p.Price == orderRequest.WashPackagePrice)
                .FirstOrDefaultAsync();
            if (package == null) return null;

            order.PackageID = package.PackageID;
            order.WashPackageName = orderRequest.WashPackageName;
            order.WashPackagePrice = orderRequest.WashPackagePrice;
            order.OrderDate = orderRequest.OrderDate;
            order.CarType = orderRequest.CarType;
            order.CarNumber = orderRequest.CarNumber;

            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Orders> CancelOrderAsync(int orderID)
        {
            var order = await _context.Orders.FindAsync(orderID);
            if (order == null) return null;

            order.OrderStatus = 2; // 2 means cancelled
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Orders>> GetOrdersByUserIdAsync(int userID)
        {
            return await _context.Orders.Where(o => o.UserID == userID).ToListAsync();
        }

        public async Task<Users> GetUsersByIdAsync(int userID){
            return await _context.Users.FindAsync(userID);
        }
    }
}
