using Car_wash.Models;

namespace Car_wash.Repository.Interface
{
    public interface IWashing
    {
        Task AssignWasherToOrderAsync(int orderId);
        Task<List<Orders>> GetAllOrdersAsync();
        Task<Orders> GetOrderByIdAsync(int orderId);
    }
}
