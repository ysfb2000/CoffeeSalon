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
            return View();
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

        public IActionResult DeleteReview(Review review)
        {
            var result = _reviewService.DeleteReview(review);
            return RedirectToAction("ReviewsAdmin", "Admin");
        }

        public IActionResult UpdateReview(Review review)
        {
            var result = _reviewService.UpdateReview(review);
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

                _reviewService.AddReview(review);

                return RedirectToAction("AddReviewInAdmin", "Admin");
            }

            return View(review);
        }
    }
}
