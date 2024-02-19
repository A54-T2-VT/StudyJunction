using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.TeacherCandidacy;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Web.Areas.Student.Controllers
{
	[Area("Student")]
    [Authorize(Roles = RolesConstants.Student)]
    public class TeacherCandidacyController : Controller
	{
		private readonly ITeacherCandidacyService teacherCandidacyService;
		private readonly UserManager<UserDb> userManager;

		public TeacherCandidacyController(ITeacherCandidacyService teacherCandidacyService
			, UserManager<UserDb> userManager)
        {
			this.teacherCandidacyService = teacherCandidacyService;
			this.userManager = userManager;
		}

		[HttpGet]
		public IActionResult Create()
		{
			var model = new AddTeacherCandidacyViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(AddTeacherCandidacyViewModel model)
		{
			try
			{
				string email = HttpContext.Session.GetString("email");

				var user = await userManager.FindByEmailAsync(email);

				await teacherCandidacyService.Create(model, user);

				return RedirectToAction("Index", "Home", new { area = RolesConstants.Student });
			}
			catch (Exception ex)
			{
				return RedirectToAction("Error", "Home", new { area = RolesConstants.Student });
			}
		}


	}
}
