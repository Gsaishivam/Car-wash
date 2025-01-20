using Car_wash.Models;
namespace Car_wash.Repository.Interface
{
    public interface IHelper
    {
        Task<Users> GetUserByOrderIdAsync(int orderId);
        Task<Washers> GetWasherByOrderIdAsync(int orderId);
        Task<Orders> GetOrderByIdAsync(int orderId);
        Task SaveChangesAsync();
    }
}
