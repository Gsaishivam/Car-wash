using Car_wash.Models;
using Car_wash.Data.DTO;
namespace Car_wash.Repository.Interface
{
    public interface ICheckout
    {
        Task<bool> ProcessCheckoutAsync(CheckoutDTO checkout);
        Task<Orders> GetOrderByIdAsync(int orderID);
        Task<Users> GetUserByIdAsync(int userID);
    }
}

