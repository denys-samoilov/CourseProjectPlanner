using CourseProjectPlanner.Models;
using CourseProjectPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public int GetUserIdFromCookies()
        {
			int userId;
			if (Request.Cookies.TryGetValue("UserId", out string userIdString))
			{
				userId = Int32.Parse(userIdString);
			}
			else
			{
				userId = 0;
			}
            ViewBag.UserId = userId;

			return userId;
        }

        public IActionResult Index()
		{

            if (GetUserIdFromCookies() != 0)
            {
                return RedirectToAction("Spends");
            }

            return View();
		}

		public IActionResult Privacy()
		{
            GetUserIdFromCookies();

            return View();
		}

		public IActionResult Spends()
		{
            if (GetUserIdFromCookies() == 0)
            {
                return RedirectToAction("Login");
            }

            var users = _User.GetUsers;
			ViewBag.UsersList = users.ToList();

			var categories = _Category.GetCategories;
			ViewBag.CategoriesList = categories.ToList();

            int userId = GetUserIdFromCookies();


            return View(_Spend.GetSpends.OrderByDescending(s => s.SpendId).Where(s => s.UserId == userId));
		}

		public IActionResult Statistics(int months)
		{
			if (GetUserIdFromCookies() == 0)
			{
				return RedirectToAction("Login");
			}
			
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
				int userId = GetUserIdFromCookies();
				DateTime selectedMonthsAgo = DateTime.Now.AddMonths(-months);
				return View(_Spend.GetSpends.Where(s => s.UserId == userId && s.SpendDate >= selectedMonthsAgo));
			
		}



		public IActionResult Login()
		{
            GetUserIdFromCookies();

            var users = _User.GetUsers;
			ViewBag.UsersIds = users.Select(u => u.UserId).ToList();
			ViewBag.UsersLogins = users.Select(u => u.Login).ToList();
			ViewBag.UsersPasswords = users.Select(u => u.Password).ToList();
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
            GetUserIdFromCookies();

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
            if (GetUserIdFromCookies() == 0)
            {
                return RedirectToAction("Login");
            }

            var categories = _Category.GetCategories.Where(s => s.RefersTo == "Spends");
			ViewBag.CategoriesList = categories.ToList();

            GetUserIdFromCookies();

            return View();
		}

		[HttpPost]
		public IActionResult AddSpend(Spend model)
		{
			if (ModelState.IsValid)
			{
				_Spend.Add(model);
				return RedirectToAction("Spends");

			}
			return View(model);
		}

		[HttpGet]
		public IActionResult EditSpend(int id)
		{
            if (GetUserIdFromCookies() == 0)
            {
                return RedirectToAction("Login");
            }

            var categories = _Category.GetCategories;
			ViewBag.CategoriesList = categories.ToList();

			if(_Spend.GetSpend(id).UserId != GetUserIdFromCookies())
			{
				return RedirectToAction("Spends");
			}

            var model = _Spend.GetSpend(id);
			return View(model);
		}

		[HttpPost]
		public IActionResult EditSpend(Spend model)
		{
			if (ModelState.IsValid)
			{
				_Spend.Edit(model);
				return RedirectToAction("Spends");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult RemoveSpend(int id)
		{
            if (GetUserIdFromCookies() == 0)
            {
                return RedirectToAction("Login");
            }
            if (_Spend.GetSpend(id).UserId != GetUserIdFromCookies())
            {
                return RedirectToAction("Spends");
            }
            var model = _Spend.GetSpend(id);
			return View(model);
		}

		[HttpPost]
		public IActionResult RemoveSpend(Spend model)
		{
			_Spend.Remove(model.SpendId);
			return RedirectToAction("Spends");
		}


		
	}
    
}

