﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.RequestDTOs.User;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.User;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;

namespace StudyJunction.Web.Areas.Student.Controllers
{
    [Area(RolesConstants.Student)]
    [Authorize(Roles = RolesConstants.Student)]
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

                if (!result.Succeeded)
                {
                    throw new InvalidCredentialsException(string.Format(ExceptionMessages.INVALID_CREDENTIALS_MESSAGE));
                }

                //setting up session variables
                HttpContext.Session.SetString("user", user.UserName);
                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetString("id", user.Id.ToString());

                return RedirectToAction("Index", "Home");
            }
            catch (InvalidCredentialsException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("user");
			HttpContext.Session.Remove("email");
			HttpContext.Session.Remove("id");
            await signInManager.SignOutAsync();

            // Redirect to a specific page after sign-out if needed
            return RedirectToAction("Index", "Home", new {area =""});
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

                return RedirectToAction("Login", "Users");
            }
            catch (NameDuplicationException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Register");
            }
        }
    }
}
