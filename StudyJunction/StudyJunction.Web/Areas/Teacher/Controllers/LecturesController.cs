using Microsoft.AspNetCore.Mvc;

namespace StudyJunction.Web.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class LecturesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
