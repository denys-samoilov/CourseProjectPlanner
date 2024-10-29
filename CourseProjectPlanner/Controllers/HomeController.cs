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
		private readonly ISpend _Spend;
		private readonly ICategory _Category;


		public HomeController(ILogger<HomeController> logger, IUser user, ICategory category, ISpend spend)
		{
			_logger = logger;
			_User = user;
			_Category = category;
			_Spend = spend;
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
			var users = _User.GetUsers;
			ViewBag.UsersList = users.ToList();

			var categories = _Category.GetCategories;
			ViewBag.CategoriesList = categories.ToList();

			int userId = Int32.Parse(Request.Cookies["UserId"]);
			ViewBag.UserId = userId;

			return View(_Spend.GetSpends.OrderByDescending(s => s.SpendId).Where(s => s.UserId == userId));
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
			var categories = _Category.GetCategories;
			ViewBag.CategoriesList = categories.ToList();
			int userId = Int32.Parse(Request.Cookies["UserId"]);
			return View(_Spend.GetSpends.Where(s => s.UserId == userId));
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
				return RedirectToAction("Login");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult AddSpend()
		{
			var categories = _Category.GetCategories;
			ViewBag.CategoriesList = categories.ToList();

			var userId = Int32.Parse(Request.Cookies["UserId"]);
			ViewBag.UserId = userId;
			return View();
		}

		public IActionResult AddSpend(Spend model)
		{
			if (ModelState.IsValid)
			{
				_Spend.Add(model);
				return RedirectToAction("Spends");

			}
			return View(model);
		}
	}
}
