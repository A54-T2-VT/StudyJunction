using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels;

namespace StudyJunction.Web.Controllers
{
	public class CoursesController : Controller
	{
		private readonly ICourseService courseService;
		private readonly IUserService userService;
		private readonly IMapper mapper;
        private readonly CloudinaryService cloudinaryService;
        public CoursesController(ICourseService _courseService, IUserService _userService, IMapper _mapper,
            CloudinaryService _cloudinaryService)
        {
            courseService = _courseService;
			userService = _userService;
			mapper = _mapper;
			cloudinaryService = _cloudinaryService;
        }
        public async Task<IActionResult> Index()
		{
			CourseViewModel courses = new CourseViewModel()
			{
				Courses = await courseService.GetAll(),
				Service = cloudinaryService
			};
			return View(courses);
		}
	}
}
