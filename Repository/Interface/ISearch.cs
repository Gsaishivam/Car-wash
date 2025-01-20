using Car_wash.Models;
using Car_wash.Data.DTO;

namespace Car_wash.Repository.Interface
{
    public interface ISearch
    {
        Task<List<WashPackages>> SearchByParams(Search searchParams);
    }
}
