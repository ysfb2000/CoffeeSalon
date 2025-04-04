using CoffeeSalon.Models;
using CoffeeSalon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeSalon.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUsersServices _userServices;
        private readonly IReviewService _reviewService;

        public AdminController(ILogger<HomeController> logger, IUsersServices userServices, IReviewService reviewService)
        {
            _userServices = userServices;
            _reviewService = reviewService;
        }

        public IActionResult AddReviewInAdmin()
        {
            var review = new Review
            {
                ItemName = string.Empty,
                ReviewText = string.Empty,
                ReviewId = 0
            };

            ViewData["Title"] = "Add";

            return View("AddReviewInAdmin", review);
        }

        public IActionResult UsersAdmin()
        {
            var list = _userServices.GetUserList().Value;

            return View(list);
        }

        public IActionResult ReviewsAdmin()
        {
            var list = _reviewService.GetReviewList().Value;
            return View(list);
        }

        public IActionResult SetAsAdmin(string userId)
        {
            var result = _userServices.SetAsAdmin(userId);
            return RedirectToAction("UsersAdmin", "Admin");
        }

        public IActionResult SetAsUser(string userId)
        {
            var result = _userServices.SetAsUser(userId);
            return RedirectToAction("UsersAdmin", "Admin");
        }

        public IActionResult DeleteUser(string userId)
        {
            var result = _userServices.DeleteUser(userId);
            return RedirectToAction("UsersAdmin", "Admin");
        }

        public IActionResult AddReview(Review review)
        {
            
            return RedirectToAction("ReviewsAdmin", "Admin");
        }

        public IActionResult DeleteReview(string reviewId)
        {
            var result = _reviewService.DeleteReview(int.Parse(reviewId));
            return RedirectToAction("ReviewsAdmin", "Admin");
        }


        public IActionResult GetReviewById(int id)
        {
            var result = _reviewService.GetReviewById(id);
            return View(result.Value);
        }

        // POST: /Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await ImageFile.CopyToAsync(memoryStream);
                    review.Image = memoryStream.ToArray();
                }

                review.DatePosted = DateTime.Now;
                review.UserId = int.Parse(HttpContext.Session.GetString("UserId") ?? "");

                _reviewService.AddReview(review);

                return RedirectToAction("AddReviewInAdmin", "Admin");
            }

            return View(review);
        }

        public IActionResult UpdateReview(int id)
        {
            var review = _reviewService.GetReviewById(id).Value;
            if (review == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit";

            return View("AddReviewInAdmin", review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Review review, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                var existingReview = _reviewService.GetReviewById(review.ReviewId).Value;
                if (existingReview == null) return NotFound();

                // Update fields
                existingReview.ItemName = review.ItemName;
                existingReview.Rating = review.Rating;
                existingReview.ReviewText = review.ReviewText;

                if (ImageFile != null)
                {
                    using var stream = new MemoryStream();
                    await ImageFile.CopyToAsync(stream);
                    existingReview.Image = stream.ToArray();
                }

                _reviewService.UpdateReview(existingReview);


                return RedirectToAction("ReviewsAdmin", "Admin");
            }

            return View("AddReviewInAdmin", review);
        }
    }
}
