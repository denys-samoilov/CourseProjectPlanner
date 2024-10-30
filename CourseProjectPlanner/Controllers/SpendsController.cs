using CourseProjectPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseProjectPlanner.Controllers
{
    public class SpendsController : Controller
    {
		private readonly ISpend _Spend;
		private readonly IUser _User;
		private readonly ICategory _Category;
		public SpendsController(ISpend spend, IUser user, ICategory category)
		{
			this._Spend = spend;
			this._User = user;
			this._Category = category;
		}
		public IActionResult Index()
        {
			var users = _User.GetUsers;
			ViewBag.UserLogins = users.ToList();

			var categories = _Category.GetCategories;
			ViewBag.CategoryNames = categories.ToList();
			return View(_Spend.GetSpends);
        }

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Create(Spend model)
		{
			if (ModelState.IsValid)
			{
				_Spend.Add(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Edit(int id) 
		{
			var model = _Spend.GetSpend(id);
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(Spend model) 
		{
			if (ModelState.IsValid)
			{
				_Spend.Edit(model);
				return RedirectToAction("Spends");
			}
			return View(model);

		}
	}
}
