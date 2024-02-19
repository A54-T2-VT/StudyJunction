using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Web.Areas.God.Controllers
{
    [Area(RolesConstants.God)]
    [Authorize(Roles = RolesConstants.God)]
    public class TeacherCandidacyController : Controller
    {
        private readonly ITeacherCandidacyService teacheerCandidacyService;
        private readonly UserManager<UserDb> userManager;

        public TeacherCandidacyController(ITeacherCandidacyService teacheerCandidacyService,
            UserManager<UserDb> userManager)
        {
            this.teacheerCandidacyService = teacheerCandidacyService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var candidacies = await teacheerCandidacyService.GetAll();

            return View("ShowAll", candidacies);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string candidacyId, string userEmail)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(userEmail);

                var result = await userManager.AddToRoleAsync(user, RolesConstants.Teacher);

                if (!result.Succeeded)
                {
                    throw new Exception();
                }

                await teacheerCandidacyService.Approve(new Guid(candidacyId));

                return RedirectToAction("GetAll", "TeacherCandidacy", new { area = RolesConstants.God });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { area = RolesConstants.God });
            }
        }
    }
}
