using Car_wash.Data.DTO;
using Car_wash.Models;
namespace Car_wash.Repository.Interface
{
    public interface IReview
    {
        Task<Reviews> AddReviewAsync(ReviewsDTO reviewDTO);
        Task<bool> DeleteReviewAsync(int reviewId);
        Task<List<ReviewsDTO>> GetReviewsGivenByUserAsync(int userId);
        Task<List<ReviewsDTO>> GetReviewsForWasherAsync(int washerId);
    }
}
