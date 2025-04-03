using System.Diagnostics;
using CoffeeSalon.Data;
using CoffeeSalon.Models;
using CoffeeSalon.Services;
using Microsoft.AspNetCore.Mvc;
using UsersApp.ViewModels;

namespace CoffeeSalon.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersServices _userServices;

        public HomeController(ILogger<HomeController> logger, IUsersServices userServices)
        {
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            return View();
        }

        public IActionResult Detail()
        {
            return View();
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

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userServices.Login(model.UserName, model.Password);

                if (result.IsSuccess)
                {
                    HttpContext.Session.SetString("UserName", model.UserName);
                    HttpContext.Session.SetString("Role", result.Value);
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
            return View();
        }

        public IActionResult ReviewsAdmin()
        {
            return View();
        }
    }
}
