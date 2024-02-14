using Microsoft.AspNetCore.Mvc;

namespace StudyJunction.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
