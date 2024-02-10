using Microsoft.AspNetCore.Mvc;

namespace StudyJunction.Web.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
