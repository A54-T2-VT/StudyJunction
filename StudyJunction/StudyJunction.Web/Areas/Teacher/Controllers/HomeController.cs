using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ViewModels;
using StudyJunction.Infrastructure.Constants;
using System.Diagnostics;

namespace StudyJunction.Web.Areas.Teacher.Controllers
{
    [Area(RolesConstants.Teacher)]
	[Authorize(Roles = RolesConstants.Teacher)]
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			return View();
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
