using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Web.Areas.Admin.Controllers
{
    [Area(RolesConstants.Admin)]
    [Authorize(Roles = RolesConstants.Admin)]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<UserDb> userManager;

        public UsersController(IUserService userService,
            UserManager<UserDb> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }



        [HttpGet]
        public async Task<IActionResult> UsersRoles()
        {
            try
            {
                var usersWithHighestRoles = await userService.GetUsersAndTheirHighestRole();

                var currUserEmail = HttpContext.Session.GetString("email");

                usersWithHighestRoles.RemoveAll(ur => ur.RoleName == RolesConstants.God || ur.RoleName == RolesConstants.Admin || ur.Email == currUserEmail);

                usersWithHighestRoles = usersWithHighestRoles.OrderBy(u => u.Email).ToList();

                return View(usersWithHighestRoles);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { area = RolesConstants.Admin });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string email)
        {
            try
            {
                var targetUser = await userManager.FindByEmailAsync(email);

                _ = await userService.IncreaseRole(targetUser.Id, RolesConstants.Admin);

                return RedirectToAction("UsersRoles", "Users", new { area = RolesConstants.Admin });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { area = RolesConstants.Admin });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string email)
        {
            try
            {
                var targetUser = await userManager.FindByEmailAsync(email);

                _ = await userService.DecreaseRole(targetUser.Id);

                return RedirectToAction("UsersRoles", "Users", new { area = RolesConstants.Admin });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { area = RolesConstants.Admin });
            }
        }
    }
}
