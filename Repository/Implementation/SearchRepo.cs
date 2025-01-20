using Microsoft.EntityFrameworkCore;
using Car_wash.Models;
using Car_wash.Data.DTO;
using Car_wash.Repository.Interface;

namespace Car_wash.Repository.Implementation
{
    public class SearchRepo : ISearch
    {
        private readonly CarWashDBContext _context;

        public SearchRepo(CarWashDBContext context)
        {
            _context = context;
        }

        public async Task<List<WashPackages>> SearchByParams(Search searchParams)
        {
            IQueryable<WashPackages> query = _context.WashPackages;

            if (!string.IsNullOrEmpty(searchParams?.name))
            {
                query = query.Where(l => l.Name.Contains(searchParams.name)); // Using Contains for partial match
            }
            if (searchParams?.price != null)
            {
                query = query.Where(l => l.Price == searchParams.price);
            }
            if (!string.IsNullOrEmpty(searchParams?.description))
            {
                query = query.Where(l => l.Description.Contains(searchParams.description)); // Using Contains for partial match
            }

            return await query.ToListAsync();
        }
    }
}
