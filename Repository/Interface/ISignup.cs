using Car_wash.Data.DTO;

namespace Car_wash.Repository.Interface
{
    public interface ISignup{
        Task<string> Washer_Signup(Signup x);
        Task<string> User_Signup(Signup y);
    }
}