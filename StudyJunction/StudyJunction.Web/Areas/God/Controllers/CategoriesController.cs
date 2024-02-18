using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Categories;
using StudyJunction.Infrastructure.Constants;

namespace StudyJunction.Web.Areas.God.Controllers
{
	[Area(RolesConstants.God)]
	public class CategoriesController : Controller
	{
		private readonly ICategoryService categoryService;

		public CategoriesController(ICategoryService categoryService)
        {
			this.categoryService = categoryService;
		}

		[HttpGet]
		public IActionResult GetMainCategories() 
		{
			throw new NotImplementedException();
		}


        [HttpGet]
        public async Task<IActionResult> Add()
        {
			var model =  await categoryService.GetAllParentChildCategoriesForSelectingCategory();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel model)
		{
			await categoryService.CreateCategoryFromViewModel(model);

            return RedirectToAction("Add", "Categories", new {area = RolesConstants.God});
		}
	}
}
