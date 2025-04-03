﻿using CoffeeSalon.Models;

namespace CoffeeSalon.Services
{
    public interface IReviewService
    {
        public Result<List<Review>> GetReviewList();
        public Result AddReview(Review review);
        public Result DeleteReview(Review review);
        public Result UpdateReview(Review review);

        public Result<Review> GetReviewById(int userId);
    }
}
