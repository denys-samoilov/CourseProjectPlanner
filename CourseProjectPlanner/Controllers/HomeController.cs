using CourseProjectPlanner.Models;
using CourseProjectPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CourseProjectPlanner.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUser _User;

		public HomeController(ILogger<HomeController> logger, IUser user)
		{
			_logger = logger;
			_User = user;
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

		

		public IActionResult Login()
		{
			var users = _User.GetUsers;
			ViewBag.UsersIds = users.Select(u => u.UserId).ToList();
			ViewBag.UsersLogins = users.Select(u => u.Login).ToList();
			ViewBag.UsersPasswords = users.Select(u => u.Password).ToList();
			return View();
		}

		public IActionResult Modal()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


        [HttpGet]
        public IActionResult Registration()
        {
			var users = _User.GetUsers;
			ViewBag.UsersLogins = users.Select(u => u.Login).ToList();
			return View();
		}

        public IActionResult Registration(User model)
        {
            if (ModelState.IsValid)
            {
                _User.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
