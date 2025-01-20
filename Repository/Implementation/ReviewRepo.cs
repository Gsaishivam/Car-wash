using Microsoft.EntityFrameworkCore;
using Car_wash.Models;
using Car_wash.Data.DTO;
using Car_wash.Repository.Interface;

namespace Car_wash.Repository.Implementation
{
    public class ReviewRepo : IReview
    {
        private readonly CarWashDBContext _context;

        public ReviewRepo(CarWashDBContext context)
        {
            _context = context;
        }

        public async Task<Reviews> AddReviewAsync(ReviewsDTO reviewDTO)
        {
            var order = await _context.Orders.FindAsync(reviewDTO.OrderID);
            if (order == null) return null;

            var existingReview = await _context.Reviews.FirstOrDefaultAsync(r => r.OrderID == reviewDTO.OrderID);
            if (existingReview != null) return null;

            var review = new Reviews
            {
                OrderID = reviewDTO.OrderID,
                Rating = reviewDTO.Rating,
                Review_comment = reviewDTO.Review_comment,
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return review;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReviewsDTO>> GetReviewsGivenByUserAsync(int userId)
        {
            var reviews = await _context.Reviews.Where(r => r.Orders.UserID == userId).Select(r => new ReviewsDTO
                {
                    OrderID = r.OrderID,
                    Rating = r.Rating, 
                    Review_comment = r.Review_comment
                })
                .ToListAsync();

            return reviews;
        }

        public async Task<List<ReviewsDTO>> GetReviewsForWasherAsync(int washerId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.Orders.WasherID == washerId)
                .Select(r => new ReviewsDTO
                {
                    OrderID = r.OrderID,
                    Rating = r.Rating,
                    Review_comment = r.Review_comment
                })
                .ToListAsync();

            return reviews;
        }
}
}
