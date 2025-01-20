using System.ComponentModel.DataAnnotations;

namespace Car_wash.Data.DTO
{    
    public class WashPackagesEdit
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
    }
}