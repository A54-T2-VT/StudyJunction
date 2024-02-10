using Microsoft.AspNetCore.Mvc;

namespace StudyJunction.Web.Controllers
{
    public class LecturesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
