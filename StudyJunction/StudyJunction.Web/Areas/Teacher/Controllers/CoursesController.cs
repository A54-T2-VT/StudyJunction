using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.RequestDTOs.Course;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Courses;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Exceptions;

namespace StudyJunction.Web.Areas.Teacher.Controllers
{
    [Area(RolesConstants.Teacher)]
    [Authorize(Roles = RolesConstants.Teacher)]
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
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string username = HttpContext.Session.GetString("user");

            var course = mapper.Map<AddCourseRequestDto>(viewModel);

            _ = await courseService.Create(course, username);
            
            return RedirectToAction("GetCoursesCreatedByUser", "Courses", new {area = RolesConstants.Teacher});
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchValue)
        {
            CourseViewModel viewModel = new CourseViewModel()
            {
                Service = cloudinaryService,
                Users = await userService.GetAll()
            };
            if (searchValue != null)
            {
                viewModel.Courses = await courseService.FilterByTitle(searchValue);
            }
            else
            {
                viewModel.Courses = await courseService.GetAll();
            }

            return View(viewModel);
        }

        [HttpGet("Teacher/Courses/Details/{title}")]
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
            var user = await userService.GetByUsername(User.Identity.Name);
            var viewmodel = new MyCoursesViewModel()
            {
                CurrentUser = user,
                Service = cloudinaryService
            };

            return View(viewmodel);
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursesCreatedByUser()
        {
            try
            {
			    string userId = HttpContext.Session.GetString("id");

                var models = await courseService.GetCoursesCreatedByUserAsync(userId);

                return View("CreatedByUserCourses", models);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { area = RolesConstants.Teacher });
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
    }
}
