using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.Services;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Courses;
using StudyJunction.Core.ViewModels.Lectures;
using StudyJunction.Core.ViewModels.WikiSearch;
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

        [HttpGet]
        public async Task<IActionResult> GetGivenLecture(string lectureTitle)
        {
            var viewModel = await lectureService.GetAllLecturesAndSetTargetLectureAsCurrent(lectureTitle);

            return View("CurrLecture", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SearchInWiki(string searchTerm)
        {
            try
            {
                string[] result = await MediaWikiActionService.MakeMediaWikiSearchRequest(searchTerm);// 0 = snippet, 1 = Uri

                var model = new WikiResultViewModel() 
                {
                    Snippet = result[0],
                    FullWikiPageUri = result[1]
                };
            
                return PartialView("_PartialSearchInWiki", model);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return PartialView("_PartialSearchInWiki", new WikiResultViewModel() { Snippet = "Nothing found", FullWikiPageUri= "https://www.pointlesssites.com/" });
            }
        }
    }
}
