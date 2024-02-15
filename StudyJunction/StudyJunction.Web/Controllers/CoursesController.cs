using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Courses;

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
				Service = cloudinaryService,
				Users = await userService.GetAll()
			};
			return View(courses);
		}

		[HttpGet("Courses/Details/{title}")]
		public async Task<IActionResult> Details([FromRoute] string title)
		{
			DetailsViewModel detailsViewModel = new DetailsViewModel()
			{
				Course = await courseService.GetCourse(title),
				Service = cloudinaryService
			};
			
			return View(detailsViewModel);
		}
	}
}
