using CourseProjectPlanner.Models;
using CourseProjectPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Linq;

namespace CourseProjectPlanner.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUser _User;
		private readonly ISpend _Spend;
		private readonly ISaving _Saving;
		private readonly ICategory _Category;

        

		public HomeController(ILogger<HomeController> logger, IUser user, ICategory category, ISpend spend, ISaving saving)
		{
            _logger = logger;
			_User = user;
			_Category = category;
			_Spend = spend;
			_Saving = saving;
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

		public IActionResult Spends(List<int> categoryIds, List<string> currencies, string description)
		{
            if (GetUserIdFromCookies() == 0)
            {
                return RedirectToAction("Login");
            }

			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Spends");
			ViewBag.CategoriesList = categories.ToList();
			ViewBag.SelectedCategoryIds = categoryIds;
			ViewBag.SelectedCurrencies = currencies;

			int userId = GetUserIdFromCookies();

			var spendsQuery = _Spend.GetSpends.OrderByDescending(s => s.SpendId).Where(s => s.UserId == userId);

			if (!string.IsNullOrWhiteSpace(description))
			{
				spendsQuery = spendsQuery.Where(s => s.Description != null && s.Description.Contains(description));
			}

			if (categoryIds != null && categoryIds.Any()) 
				spendsQuery = spendsQuery.Where(s => categoryIds.Contains(s.CategoryId));

			if (currencies != null && currencies.Any())
				spendsQuery = spendsQuery.Where(s => currencies.Contains(s.Currency));


			return View(spendsQuery.ToList());
		}

		public IActionResult FilterSpends(List<int> categoryIds, List<string> currencies, string description)
		{
			if (GetUserIdFromCookies() == 0)
				return RedirectToAction("Login");

			return RedirectToAction("Spends", new { categoryIds = categoryIds, currencies = currencies, description = description });
		}

		public IActionResult FilterSpendsTest()
		{
			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Spends");
			ViewBag.CategoriesList = categories.ToList();
			return View();
		}

		public IActionResult Savings(List<int> categoryIds, List<string> currencies, string description)
		{
			if (GetUserIdFromCookies() == 0)
			{
				return RedirectToAction("Login");
			}

			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Savings");
			ViewBag.CategoriesList = categories.ToList();
			ViewBag.SelectedCategoryIds = categoryIds;
			ViewBag.SelectedCurrencies = currencies;

			int userId = GetUserIdFromCookies();

			var savingsQuery = _Saving.GetSavings.OrderByDescending(s => s.SavingId).Where(s => s.UserId == userId);

			if (!string.IsNullOrWhiteSpace(description))
			{
				savingsQuery = savingsQuery.Where(s => s.Description != null && s.Description.Contains(description));
			}

			if (categoryIds != null && categoryIds.Any())
				savingsQuery = savingsQuery.Where(s => categoryIds.Contains(s.CategoryId));

			if (currencies != null && currencies.Any())
				savingsQuery = savingsQuery.Where(s => currencies.Contains(s.Currency));


			return View(savingsQuery.ToList());
		}

		public IActionResult FilterSavings(int? categoryId, string currency)
		{
			if (GetUserIdFromCookies() == 0)
			{
				return RedirectToAction("Login");
			}

			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Savings");
			ViewBag.CategoriesList = categories.ToList();

			int userId = GetUserIdFromCookies();

			return RedirectToAction("Savings", new { categoryId = categoryId, currency = currency });
		}

		public IActionResult FilterSavingsTest()
		{
			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Savings");
			ViewBag.CategoriesList = categories.ToList();
			return View();
		}


		public IActionResult StatisticsSpends(int months)
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
			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Spends");
			ViewBag.CategoriesList = categories.ToList();
				int userId = GetUserIdFromCookies();
				DateTime selectedMonthsAgo = DateTime.Now.AddMonths(-months);

			var savings = _Saving.GetSavings.Where(s => s.UserId == userId && s.SavingDate >= selectedMonthsAgo);
			ViewBag.SavingsList = savings.ToList();
			return View(_Spend.GetSpends.Where(s => s.UserId == userId && s.SpendDate >= selectedMonthsAgo));
		}

		public IActionResult StatisticsSavings(int months)
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
			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Savings");
			ViewBag.CategoriesList = categories.ToList();

			int userId = GetUserIdFromCookies();
			DateTime selectedMonthsAgo = DateTime.Now.AddMonths(-months);

			var spends = _Spend.GetSpends.Where(s => s.UserId == userId && s.SpendDate >= selectedMonthsAgo);
			ViewBag.SpendsList = spends.ToList();

			return View(_Saving.GetSavings.Where(s => s.UserId == userId && s.SavingDate >= selectedMonthsAgo));
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
		public IActionResult AddSaving()
		{
			if (GetUserIdFromCookies() == 0)
			{
				return RedirectToAction("Login");
			}

			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Savings");
			ViewBag.CategoriesList = categories.ToList();

			GetUserIdFromCookies();

			return View();
		}

		[HttpPost]
		public IActionResult AddSaving(Saving model)
		{
			if (ModelState.IsValid)
			{
				_Saving.Add(model);
				return RedirectToAction("Savings");

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

			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Spends");
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
		public IActionResult EditSaving(int id)
		{
			if (GetUserIdFromCookies() == 0)
			{
				return RedirectToAction("Login");
			}

			var categories = _Category.GetCategories.Where(s => s.RefersTo == "Savings");
			ViewBag.CategoriesList = categories.ToList();

			if (_Saving.GetSaving(id).UserId != GetUserIdFromCookies())
			{
				return RedirectToAction("Savings");
			}

			var model = _Saving.GetSaving(id);
			return View(model);
		}

		[HttpPost]
		public IActionResult EditSaving(Saving model)
		{
			if (ModelState.IsValid)
			{
				_Saving.Edit(model);
				return RedirectToAction("Savings");
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

		[HttpGet]
		public IActionResult RemoveSaving(int id)
		{
			if (GetUserIdFromCookies() == 0)
			{
				return RedirectToAction("Login");
			}
			if (_Saving.GetSaving(id).UserId != GetUserIdFromCookies())
			{
				return RedirectToAction("Savings");
			}
			var model = _Saving.GetSaving(id);
			return View(model);
		}

		[HttpPost]
		public IActionResult RemoveSaving(Saving model)
		{
			_Saving.Remove(model.SavingId);
			return RedirectToAction("Savings");
		}

		public IActionResult ExchangeRate()
		{
			return View();
		}



	}
    
}

