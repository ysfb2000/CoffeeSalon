using CoffeeSalon.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeSalon.Services
{
    public interface IReviewService
    {
        public Result<List<Review>> GetReviewList();
        public Result AddReview(Review review);
        public Result DeleteReview(int reviewId);
        public Result UpdateReview(Review review);

        public Result<Review> GetReviewById(int userId);
    }
}
