using System.Diagnostics;
using CoffeeSalon.Data;
using CoffeeSalon.Models;
using CoffeeSalon.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersApp.ViewModels;

namespace CoffeeSalon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUsersServices _userServices;

        public HomeController(ILogger<HomeController> logger, IUsersServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }

        public IActionResult Index()
        {
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

                if (result)
                {
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    Users users = new Users
            //    {
            //        FullName = model.Name,
            //        Email = model.Email,
            //        UserName = model.Email,
            //    };

            //    var result = await userManager.CreateAsync(users, model.Password);

            //    if (result.Succeeded)
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }
            //    else
            //    {
            //        foreach (var error in result.Errors)
            //        {
            //            ModelState.AddModelError("", error.Description);
            //        }

            //        return View(model);
            //    }
            //}
            return View(model);
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = await userManager.FindByNameAsync(model.Email);

            //    if (user == null)
            //    {
            //        ModelState.AddModelError("", "Something is wrong!");
            //        return View(model);
            //    }
            //    else
            //    {
            //        return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
            //    }
            //}
            return View(model);
        }

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = await userManager.FindByNameAsync(model.Email);
            //    if (user != null)
            //    {
            //        var result = await userManager.RemovePasswordAsync(user);
            //        if (result.Succeeded)
            //        {
            //            result = await userManager.AddPasswordAsync(user, model.NewPassword);
            //            return RedirectToAction("Login", "Account");
            //        }
            //        else
            //        {

            //            foreach (var error in result.Errors)
            //            {
            //                ModelState.AddModelError("", error.Description);
            //            }

            //            return View(model);
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "Email not found!");
            //        return View(model);
            //    }
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Something went wrong. try again.");
            //    return View(model);
            //}

            return View(model);
        }

        public IActionResult Logout()
        {
            //await signInManager.SignOutAsync();
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
