using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Services.Contracts;

namespace StudyJunction.Web.Controllers
{
	public class CoursesController : Controller
	{
		private readonly ICourseService courseService;
		private readonly IUserService userService;
		private readonly IMapper mapper;
        public CoursesController(ICourseService _courseService, IUserService _userService, IMapper _mapper)
        {
            courseService = _courseService;
			userService = _userService;
			mapper = _mapper;
        }
        public IActionResult Index()
		{
			var courses = courseService.GetAll();
			return View(courses);
		}
	}
}
