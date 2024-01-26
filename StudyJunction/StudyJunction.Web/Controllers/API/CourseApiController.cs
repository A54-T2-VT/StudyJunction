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
        public CourseApiController(ICourseService _courseService)
        {
            courseService = _courseService;
        }

		[HttpGet("{id}")]
		public IActionResult Get(string id)
		{
			try
			{
				var course = courseService.GetCourse(new Guid(id));
				return Ok(course);
			}
			catch(EntityNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{title}")]
		public IActionResult GetByTitle(string title)
		{
			try
			{
				var course = courseService.GetCourse(title);
				return Ok(course);
			}
			catch(EntityNotFoundException e)
			{
				return BadRequest(e.Message);
			}
		}

        [HttpGet("")]
		public IActionResult GetCourses()
		{
			var courses = courseService.GetAll();
			return Ok(courses);
		}

		[HttpPost("")]
		public IActionResult CreateCourse([FromBody] AddCourseRequestDto newCourse)
		{
			try
			{
				var course = courseService.Create(newCourse);
				return Ok(course);
			}
			catch (UnauthorizedUserException e)
			{
				return Unauthorized(e.Message);
			}
			catch (NameDuplicationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public IActionResult UpdateCourse(string id, [FromBody] CourseRequestDto newData)
		{
			try
			{
				var updated = courseService.Update(new Guid(id), newData);
				return Ok(updated);
			}
			catch (UnauthorizedUserException e)
			{
				return Unauthorized(e.Message);
			}
			catch (EntityNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCourse(string id)
		{
			try
			{
				var deleted = courseService.Delete(new Guid(id));
				return Ok(deleted);
			}
			catch (UnauthorizedUserException e)
			{
				return Unauthorized(e.Message);
			}
			catch (EntityNotFoundException e)
			{
				return BadRequest(e.Message);
			}
		}
		
	}
}
