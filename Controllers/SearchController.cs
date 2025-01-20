using Microsoft.AspNetCore.Mvc;
using Car_wash.Data.DTO;
using Car_wash.Repository.Interface;
namespace Car_wash.Controllers
{
    [Route("search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearch _searchRepo;
        public SearchController(ISearch searchRepo)
        {
            _searchRepo = searchRepo;
        }
        #region Search
        [HttpGet]
        public async Task<IActionResult> SearchByParams([FromQuery] Search searchParams)
        {
            if (searchParams == null)
            {
                return BadRequest(new { message = "Invalid search parameters." });
            }
            var result = await _searchRepo.SearchByParams(searchParams);

            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No matching wash packages found." });
            }

            return Ok(result);
        }
        #endregion
    }
}