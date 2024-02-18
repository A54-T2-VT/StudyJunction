using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ViewModels;
using StudyJunction.Infrastructure.Constants;
using System.Diagnostics;

namespace StudyJunction.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (!(User!.Identity!.IsAuthenticated))
            {
                return View();
            }

            if (User.IsInRole(RolesConstants.God))
            {
                return RedirectToAction("Index", "Home", new {area = RolesConstants.God});
            }
            if (User.IsInRole(RolesConstants.Admin))
            {
                return RedirectToAction("Index", "Home", new { area = RolesConstants.Admin });
            }
            if (User.IsInRole(RolesConstants.Teacher))
            {
                return RedirectToAction("Index", "Home", new { area = RolesConstants.Teacher });
            }
            else//role must be student
            {
                return RedirectToAction("Index", "Home", new { area = RolesConstants.Student });
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
