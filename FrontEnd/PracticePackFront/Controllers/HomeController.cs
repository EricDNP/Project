using Microsoft.AspNetCore.Mvc;
using PracticePackFront.Attributes;
using PracticePackFront.Helpers;
using PracticePackFront.Models;
using System.Diagnostics;

namespace PracticePackFront.Controllers
{
    [RedirectingAction]
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

        public IActionResult Privacy()
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