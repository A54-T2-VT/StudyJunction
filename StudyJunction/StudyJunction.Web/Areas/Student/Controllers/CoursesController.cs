using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.RequestDTOs.Course;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Courses;

namespace StudyJunction.Web.Areas.Student.Controllers
{
    [Area("Student")]
    //[Authorize]
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

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateCourseViewModel();
            return View("Create", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string username = HttpContext.Session.GetString("user");

            var course = mapper.Map<AddCourseRequestDto>(viewModel);

            _ = await courseService.Create(course, username);

            var routeValues = new RouteValueDictionary
            {
                { "controller", "Courses" },
                { "action", "Details" },
                { "title", viewModel.Title }
            };

            var redirectResult = new RedirectToActionResult("Details", "Courses", routeValues);

            return redirectResult;
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
