using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Services;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Courses;
using StudyJunction.Core.ViewModels.Lectures;
using StudyJunction.Infrastructure.Constants;

namespace StudyJunction.Web.Areas.Student.Controllers
{
    [Area(RolesConstants.Student)]
    [Authorize(Roles = RolesConstants.Student)]
    public class LecturesController : Controller
    {
        private readonly ILectureService lectureService;

        public LecturesController(ILectureService lectureService)
        {
            this.lectureService = lectureService;
        }


        [HttpGet("Student/Lectures/GetFirstLecture/{title}")]
        public async Task<IActionResult> GetFirstLecture([FromRoute] string title)
        {          
            var viewModel = await lectureService.GetAllLecturesOfCourse(title);

            return View("CurrLecture", viewModel);
        }
    }
}
