using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Services;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Web.Areas.God.Controllers
{
    [Area("God")]
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
            var usersWithHighestRoles = await userService.GetUsersAndTheirHighestRole();

            var currUserEmail = HttpContext.Session.GetString("email");

            usersWithHighestRoles.RemoveAt(usersWithHighestRoles.FindIndex(ur => ur.Email == currUserEmail));

            return View(usersWithHighestRoles);
        }
    }
}
