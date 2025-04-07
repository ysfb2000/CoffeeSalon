using CoffeeSalon.Data;
using CoffeeSalon.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeSalon.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public Result<List<Review>> GetReviewList(string searchString, string star, string day)
        {
            var temp = _context.Reviews
                .Include(r => r.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(day) && day != "0")
            {
                // get the date from the last days
                DateTime date = DateTime.Now.AddDays(-int.Parse(day));
                temp = temp.Where(r => r.DatePosted >= date);
            }

            if (!string.IsNullOrEmpty(star) && star != "0")
            {
                int starValue = int.Parse(star);
                temp = temp.Where(r => r.Rating == starValue);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                temp = temp.Where(r => r.ReviewText.Contains(searchString)  || r.Category.Contains(searchString) || r.ItemName.Contains(searchString));
            }

            var result = new Result<List<Review>>();
            try
            {
                var reviews = temp.ToList();
                result.Value = reviews;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

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

        public Result DeleteReview(int reviewId)
        {
            var result = new Result();
            try
            {
                var review = _context.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
                if (review != null)
                {
                    _context.Reviews.Remove(review);
                    _context.SaveChanges();
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
