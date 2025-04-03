using System.Diagnostics;
using CoffeeSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeSalon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        //Added Login Page
        public IActionResult Login()
        {
            return View();
        }

        //Added Register Page
        public IActionResult Register()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
