using CourseProjectPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseProjectPlanner.Controllers
{
	public class SavingsController : Controller
	{
		private readonly ISaving _Saving;
		private readonly IUser _User;
		private readonly ICategory _Category;
		public SavingsController(ISaving saving, IUser user, ICategory category)
		{
			this._Saving = saving;
			this._User = user;
			this._Category = category;
		}
		public IActionResult Index()
		{
			var users = _User.GetUsers;
			ViewBag.UserLogins = users.ToList();

			var categories = _Category.GetCategories;
			ViewBag.CategoryNames = categories.ToList();
			return View(_Saving.GetSavings);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Create(Saving model)
		{
			if (ModelState.IsValid)
			{
				_Saving.Add(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var model = _Saving.GetSaving(id);
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(Saving model)
		{
			if (ModelState.IsValid)
			{
				_Saving.Edit(model);
				return RedirectToAction("Savings");
			}
			return View(model);

		}
	}
}
