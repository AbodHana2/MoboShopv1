using Microsoft.AspNetCore.Mvc;
using MoboShopv1.Models;
using MoboShopv1.Models.Interfaces;
using System.Diagnostics;

namespace MoboShopv1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unit;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unit)
        {
            _logger = logger;
            _unit = unit;
        }

        public IActionResult Index()
        {
            var products = _unit.product.GetAll();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
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