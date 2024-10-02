using CourseProjectPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CourseProjectPlanner.Controllers
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

		public IActionResult Privacy()
		{
			return View();
		}

        public IActionResult Spends()
        {
            return View();
        }

        public IActionResult Statistics(int months)
        {
            switch (months)
            {
                case 1:
                    { ViewBag.Months = $"останній {months} місяць"; break; }

                case 2:
                case 3:
                case 4:
                    { ViewBag.Months = $"останні {months} місяці"; break; }

                default:
                    { ViewBag.Months = $"останні {months} місяців"; break; }

            }

            return View();
        }

		public IActionResult Registration()
		{
			return View();
		}

		public IActionResult Login()
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
