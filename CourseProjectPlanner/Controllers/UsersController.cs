using CourseProjectPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseProjectPlanner.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUser _User;
		public UsersController(IUser user)
		{
			this._User = user;
		}
		public IActionResult Index()
		{
			return View(_User.GetUsers);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Create(User model)
		{
			if (ModelState.IsValid)
			{
				_User.Add(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Delete()
		{
			return View();
		}

		public IActionResult Delete(int id)
		{
			if (id != null)
			{
				_User.Remove(id);
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
