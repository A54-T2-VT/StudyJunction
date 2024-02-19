using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.RequestDTOs.Course;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Courses;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Exceptions;

namespace StudyJunction.Web.Areas.Student.Controllers
{
    [Area(RolesConstants.Student)]
    [Authorize(Roles = RolesConstants.Student)]
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly CloudinaryService cloudinaryService;
        public CoursesController(ICourseService _courseService, IUserService _userService, IMapper _mapper, ICategoryService _categoryService,
            CloudinaryService _cloudinaryService)
        {
            courseService = _courseService;
            userService = _userService;
            mapper = _mapper;
            categoryService = _categoryService;
            cloudinaryService = _cloudinaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateCourseViewModel();

            var parentChildCategoreis = await this.categoryService.GetAllParentChildCategoriesForSelectingCategory();

            viewModel.ParentChildCategories = parentChildCategoreis.ParentChildCategories;

            return View("Create", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel viewModel)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Create");
            }
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

        [HttpGet("Student/Courses/Details/{title}")]
        public async Task<IActionResult> Details([FromRoute] string title)
        {
            DetailsViewModel detailsViewModel = new DetailsViewModel()
            {
                Course = await courseService.GetCourse(title),
                Service = cloudinaryService,
                Username = User.Identity.Name
            };

            return View(detailsViewModel);
        }

        [HttpGet()]
        public async Task<IActionResult> MyCourses()
        {
            try
            {
                var user = await userService.GetByUsernameIncludeCourses(User.Identity.Name);
                var viewmodel = new MyCoursesViewModel()
                {
                    CurrentUser = user,
                    Service = cloudinaryService
                };

                return View(viewmodel);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EnrollForCourse(string courseTitle)
        {
            var viewModel = new DetailsViewModel()
            {
                Course = await courseService.GetCourse(courseTitle),
                Service = cloudinaryService,
                Username = User.Identity.Name
            };

            try
            {
                var result = await courseService.EnrollUserForCourse(viewModel.Username, courseTitle);
                viewModel.Course = result;
                return View("Details", viewModel);
            }
            catch (EntityNotFoundException e)
            {
                return View("Details", viewModel);
            }
        }

        [HttpGet("Student/Courses/EnrolledCourseView/{title}")]
        public async Task<IActionResult> EnrolledCourseView([FromRoute] string title)
        {
            var viewModel = new EnrolledCourseViewModel()
            {
                Course = await courseService.GetCourse(title),
                Service = cloudinaryService,

                Username = User.Identity.Name
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetLectureVideo()
        {
            return BadRequest();
            

        }
    }
}
