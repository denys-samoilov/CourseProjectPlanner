using CourseProjectPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseProjectPlanner.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategory _Category;
        public CategoriesController(ICategory category)
        {
            this._Category = category;
        }
        public IActionResult Index()
        {
            return View(_Category.GetCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _Category.Add(model);
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
				_Category.Remove(id);
				return RedirectToAction("Index");
			}
			return View();
		}


		[HttpGet]
		public IActionResult Edit()
		{
			return View();
		}
	}
}
