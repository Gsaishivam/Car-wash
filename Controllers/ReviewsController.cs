using Microsoft.AspNetCore.Mvc;
using Car_wash.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Car_wash.Repository.Interface;

namespace Car_wash.Controllers
{
    [Route("reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReview _reviewRepository;
        private readonly GenerateMail _emailService;
        private readonly IHelper _helperRepo;

        public ReviewController(IReview reviewRepository, GenerateMail emailService, IHelper helperRepo)
        {
            _reviewRepository = reviewRepository;
            _emailService = emailService;
            _helperRepo = helperRepo;
        }
        #region Add/Delete Reviews
        [Authorize(Roles = "User")]
        [HttpPost("add")]
        public async Task<IActionResult> AddReview(ReviewsDTO reviewDTO)
        {
            if (reviewDTO == null)
            {
                return BadRequest(new { message = "Review data is required." });
            }

            if (reviewDTO.Rating < 0 || reviewDTO.Rating > 10)
            {
                return BadRequest(new { message = "Rating must be between 0 and 10." });
            }

            if (string.IsNullOrEmpty(reviewDTO.Review_comment) || reviewDTO.Review_comment.Length > 500)
            {
                return BadRequest(new { message = "Review should be lower than 500 characters." });
            }

            // Add review via the repository
            var review = await _reviewRepository.AddReviewAsync(reviewDTO);
            if (review == null)
            {
                return BadRequest(new { message = "Order not found or review already exists or payment pending." });
            }

            var order = await _helperRepo.GetOrderByIdAsync(reviewDTO.OrderID);
            var user = await _helperRepo.GetUserByOrderIdAsync(reviewDTO.OrderID);
            var washer = await _helperRepo.GetWasherByOrderIdAsync(reviewDTO.OrderID);

            await _emailService.SendEmailAsync(user.Email, "Review added by you!",
                $"Dear {user.FirstName} {user.LastName},<br/>You have added a new review for washer {washer.FirstName} {washer.LastName}.");
            await _emailService.SendEmailAsync(washer.Email, "Review added for you!",
                $"Dear {washer.FirstName} {washer.LastName},<br/>You have received a new review from user {user.FirstName} {user.LastName}.");

            return Ok(new { message = "Review added successfully.", reviewID = review.ReviewID });
        }

        [Authorize(Roles = "User")]
        [HttpDelete("delete/{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var success = await _reviewRepository.DeleteReviewAsync(reviewId);
            if (!success)
            {
                return NotFound(new { message = "Review not found." });
            }

            return Ok(new { message = "Review deleted successfully." });
        }
        #endregion

        #region Washer Reviews
        [Authorize(Roles = "Washer")]
        [HttpGet("washer")]
        public async Task<IActionResult> GetReviewsForWasher(int washerId)
        {
            var reviews = await _reviewRepository.GetReviewsForWasherAsync(washerId);

            if (reviews == null || !reviews.Any())
            {
                return NotFound(new { message = "No reviews found for this washer." });
            }

            return Ok(reviews);
        }
        #endregion

        #region User Reviews
        [Authorize(Roles = "User")]
        [HttpGet("user")]
        public async Task<IActionResult> GetReviewsGivenByUser(int userId)
        {
            var reviews = await _reviewRepository.GetReviewsGivenByUserAsync(userId);

            if (reviews == null || !reviews.Any())
            {
                return NotFound(new { message = "No reviews found for this user." });
            }

            return Ok(reviews);
        }
        #endregion
    }
}
