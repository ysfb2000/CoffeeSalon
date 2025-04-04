using System.Diagnostics;
using CoffeeSalon.Data;
using CoffeeSalon.Models;
using CoffeeSalon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersApp.ViewModels;

namespace CoffeeSalon.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersServices _userServices;
        private readonly IReviewService _reviewService;

        public HomeController(ILogger<HomeController> logger, IUsersServices userServices, IReviewService reviewService)
        {
            _userServices = userServices;
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("UserName", "ysfb2000");
            //HttpContext.Session.SetString("Role", "admin");
            //HttpContext.Session.SetString("UserId", "1");

            // You can also use ViewBag to store session data if needed
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.UserId = HttpContext.Session.GetString("UserId");

            // Get the result of the reviews
            var reviewsResult = _reviewService.GetReviewList();

            // Check if the result is successful and contains reviews
            if (reviewsResult.IsSuccess && reviewsResult.Value != null)
            {
                return View(reviewsResult.Value);  // Pass the list of reviews to the view
            }

            // If unsuccessful or no reviews found, pass an empty list
            return View(new List<Review>());
        }

        //Added Contact Page Action 
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }


        public IActionResult AddReivewInAdmin()
        {
            return View();
        }

        public IActionResult AddReviewInUser()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.UserId = HttpContext.Session.GetString("UserId");
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userServices.Login(model.UserName, model.Password);

                if (result.IsSuccess)
                {
                    HttpContext.Session.SetString("UserName", model.UserName);
                    HttpContext.Session.SetString("Role", result.Value.Role);
                    HttpContext.Session.SetString("UserId", result.Value.UserId.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User name or password is incorrect.");
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Username = model.Name,
                    PasswordHash = model.Password,
                    Role = "user" // Default role
                };

                var result = _userServices.Register(model.Name, model.Password);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {

                    ModelState.AddModelError("", "Register failed");
                    
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("UsersAdmin", "Home");
        }

        public IActionResult SetAsUser(string userId)
        {
            var result = _userServices.SetAsUser(userId);
            return RedirectToAction("UsersAdmin", "Home");
        }

        public IActionResult DeleteUser(string userId)
        {
            var result = _userServices.DeleteUser(userId);
            return RedirectToAction("UsersAdmin", "Home");
        }

        public IActionResult AddReview(Review review)
        {
            var result = _reviewService.AddReview(review);
            return RedirectToAction("ReviewsAdmin", "Home");
        }

        public IActionResult DeleteReview(int reviewId)
        {
            var result = _reviewService.DeleteReview(reviewId);
            return RedirectToAction("ReviewsAdmin", "Home");
        }

        public IActionResult UpdateReview(Review review)
        {
            var result = _reviewService.UpdateReview(review);
            return RedirectToAction("ReviewsAdmin", "Home");
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

                return RedirectToAction("Index", "Home");
            }

            return View(review);
        }

        public IActionResult Detail(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.UserId = HttpContext.Session.GetString("UserId");

            // Fetch the review details by ReviewId
            var result = _reviewService.GetReviewById(id);

            if (!result.IsSuccess || result.Value == null)
            {
                return NotFound(); // Return a 404 if the review is not found
            }

            // Extract the Review object from Result<Review>
            var review = result.Value;

            // Pass only the Review object to the view
            return View(review);
        }


    }
}
