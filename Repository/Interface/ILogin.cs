using Car_wash.Data.DTO;
using Car_wash.Models;
namespace Car_wash.Repository.Interface
{
    public interface ILogin{
        Task<string> Login(Login y);
        Task<string> Logout();
        Task<Users> GetUserByEmail(string email);
        Task<Washers> GetWasherByEmail(string email);
        string GenerateJwtToken(string email, string role,int userID);
    }
}