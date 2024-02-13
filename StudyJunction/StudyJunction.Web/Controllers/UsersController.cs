using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.RequestDTOs.User;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.User;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;

namespace StudyJunction.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService,
            IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string delete)
        {
            throw new NotFiniteNumberException();
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
            try
            {
                var registerDto = mapper.Map<RegisterUserRequestDto>(model);

                _ = await userService.Register(registerDto);

                return RedirectToAction("Login","Users");
            }
            catch(NameDuplicationException ex) 
            {
                throw new NotImplementedException();
            }
        }
    }
}
