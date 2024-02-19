using Microsoft.AspNetCore.Mvc;
using StudyJunction.Infrastructure.Constants;

namespace StudyJunction.Web.Areas.Teacher.Controllers
{
    [Area(RolesConstants.Teacher)]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
