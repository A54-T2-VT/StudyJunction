using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Exceptions;

namespace StudyJunction.Web.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseApiController : ControllerBase
	{
		private ICourseService courseService;
        public CourseApiController()
        {
            
        }
        [HttpGet("")]
		public IActionResult GetCourses()
		{
			var courses = courseService.GetAll();
			return Ok(courses);
		}
		[HttpPost("")]
		public IActionResult CreateCourse([FromBody]AddCourseRequestDto newCourse)
		{
			try
			{
				var course = courseService.Create(newCourse);
				return Ok(course);
			}
			catch(NameDuplicationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
