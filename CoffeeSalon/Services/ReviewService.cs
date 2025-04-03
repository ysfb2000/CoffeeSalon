using CoffeeSalon.Data;
using CoffeeSalon.Models;

namespace CoffeeSalon.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public Result<List<Review>> GetReviewList()
        {
            var list = _context.Reviews.ToList();
            var result = new Result<List<Review>>();
            result.Value = list;
            return result;
        }

        public Result AddReview(Review review)
        {
            var result = new Result();
            try
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Result DeleteReview(Review review) 
        {
            var result = new Result();
            try
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Result UpdateReview(Review review)
        {
            var result = new Result();
            try
            {
                _context.Reviews.Update(review);
                _context.SaveChanges();
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<Review> GetReviewById(int id)
        {
            var result = new Result<Review>();
            try
            {
                var review = _context.Reviews.FirstOrDefault(r => r.ReviewId == id);
                if (review != null)
                {
                    result.Value = review;
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Review not found";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }



    }
}
