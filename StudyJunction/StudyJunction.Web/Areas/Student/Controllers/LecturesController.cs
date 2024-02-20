using Microsoft.AspNetCore.Mvc;

namespace StudyJunction.Web.Areas.Student.Controllers
{
    public class LecturesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
