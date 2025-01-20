using Car_wash.Models;
using Car_wash.Data.DTO;
namespace Car_wash.Repository.Interface
{
    public interface IAdmin
    {
        Task<string> AdminLoginAsync(string email, string password);
        Task<IEnumerable<Users>> GetUsersAsync();
        Task<Users> GetUserByIdAsync(int id);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> DeleteOrderAsync(int orderID);
        Task<IEnumerable<Orders>> GetAllOrdersAsync();
        Task<Orders> GetOrderByIdAsync(int id);
        Task<IEnumerable<Washers>> GetWashersAsync();
        Task<Washers> GetWasherByIdAsync(int id);
        Task<bool> DeleteWasherAsync(int id);
        Task<IEnumerable<dynamic>> GetLeaderboardAsync();
        Task<bool> AddPackageAsync(WashPackages package);
        Task<bool> EditPackageAsync(int id, WashPackagesEdit packageEdit);
        Task<bool> DeletePackageAsync(int id);
        Task<IEnumerable<WashPackages>> GetPackagesAsync();
    }
}
