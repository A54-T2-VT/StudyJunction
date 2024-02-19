using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using StudyJunction.Core.RequestDTOs.User;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.User;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;

namespace StudyJunction.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserManager<UserDb> userManager;
        private readonly SignInManager<UserDb> signInManager;

        public UsersController(IUserService userService,
            IMapper mapper,
            UserManager<UserDb> userManager,
            SignInManager<UserDb> signInManager)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //Fix exception handling
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                //setting up session variables
                HttpContext.Session.SetString("user", user.UserName);
                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetString("id", user.Id.ToString());

                if (!result.Succeeded)
                {
                    throw new InvalidCredentialsException(string.Format(ExceptionMessages.INVALID_CREDENTIALS_MESSAGE));
                }

                var roles = await userManager.GetRolesAsync(user);

                if(roles.Contains("God"))
                {
                    return RedirectToAction("Index", "Home", new { area = RolesConstants.God });
                }
                else if (roles.Contains("Admin"))
                {
					return RedirectToAction("Index", "Home", new { area = RolesConstants.Admin });
				}
                else if (roles.Contains("Teacher"))
                {
					return RedirectToAction("Index", "Home", new { area = RolesConstants.Teacher });
				}
                else if (roles.Contains("Student"))
                {
                    return RedirectToAction("Index", "Home", new { area = RolesConstants.Student });
                }


                return RedirectToAction("Index", "Home");
            }
            catch(InvalidCredentialsException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("user");
			HttpContext.Session.Remove("email");
			HttpContext.Session.Remove("id");
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //Fix exception handling
            try
            {
                var registerDto = mapper.Map<RegisterUserRequestDto>(model);

                _ = await userService.Register(registerDto);

                return RedirectToAction("Login","Users");
            }
            catch(NameDuplicationException ex) 
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}
