using CourseProjectPlanner.Models;
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
            return View(_Spend.GetSpends);
        }

		[HttpGet]
		public IActionResult Create()
		{
            ViewBag.Users = _User.GetUsers;
            ViewBag.Categories = _Category.GetCategories;
            return View();
		}



		[HttpPost]
		public IActionResult Create(Spend model)
		{
			
			if (ModelState.IsValid)
			{
				ViewBag.Users = _User.GetUsers;
				ViewBag.Categories = _Category.GetCategories;
				_Spend.Add(model);
				return RedirectToAction("Index");
			}
			ViewBag.Users = _User.GetUsers;
			ViewBag.Categories = _Category.GetCategories;
			return View(model);
		}
	}
}
