using Car_wash.Models;
using Car_wash.Data.DTO;
using System.Security.Claims;

namespace Car_wash.Repository.Interface
{
    public interface IOrder
    {
        Task<Orders> AddOrderAsync(OrderDTO orderRequest,ClaimsPrincipal userClaims);
        Task<Orders> UpdateOrderAsync(int orderID, OrderDTO orderRequest);
        Task<Orders> CancelOrderAsync(int orderID);
        Task<List<Orders>> GetOrdersByUserIdAsync(int userID);
        Task<Users> GetUsersByIdAsync(int userID);
    }
}
