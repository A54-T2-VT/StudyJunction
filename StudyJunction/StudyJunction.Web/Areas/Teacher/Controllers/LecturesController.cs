using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Lectures;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Web.Areas.Teacher.Controllers
{
    [Area(RolesConstants.Teacher)]
    [Authorize(Roles = RolesConstants.Teacher)]
    public class LecturesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ILectureService lectureService;
        private readonly CloudinaryService cloudService;

        public LecturesController(ICourseService courseService,
            ILectureService lectureService,
            CloudinaryService cloudService)
        {
            this.courseService = courseService;
            this.lectureService = lectureService;
            this.cloudService = cloudService;
        }

        [HttpGet]
        public IActionResult Add(string courseTitle)
        {
            //var course = await courseService.GetCourse(courseTitle);

            var model = new AddLectureViewModel();

            model.CourseTitle = courseTitle;

            return View(model);
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> Add(AddLectureViewModel model) 
        {
            try
            {
                string username = HttpContext.Session.GetString("user") ?? throw new EntityNotFoundException("String in session with key 'user' does not exists");

                _ = await lectureService.CreateWithVideoAndAssignmentFromViewModel(model, username);

                return RedirectToAction("GetCoursesCreatedByUser", "Courses", new { area = RolesConstants.Teacher });
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetCoursesCreatedByUser", "Courses", new { area = RolesConstants.Teacher });
            }

        }
    }
}
