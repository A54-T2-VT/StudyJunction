using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.RequestDTOs.Lecture;
using StudyJunction.Core.Services;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using StudyJunction.Web.CustomAttributes;
using System.Security.Claims;

namespace StudyJunction.Web.Controllers.API
{
    [Route("api/lectures")]
	[ApiController]
	public class LecturesApiController : ControllerBase
	{
		private ILectureService lectureService;
        public LecturesApiController(ILectureService _lectureService)
        {
            lectureService = _lectureService;
        }

        [HttpGet("{id}")]
		public IActionResult GetById(string id)
		{
			try
			{
				var lecture = lectureService.Get(new Guid(id));
				return Ok(lecture);
			}
			catch (EntityNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{title}")]
		public IActionResult GetByTitle(string title)
		{
			try
			{
				var lecture = lectureService.Get(title);
				return Ok(lecture);
			}
			catch (EntityNotFoundException e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("")]
		public IActionResult GetCourses()
		{
			var courses = lectureService.GetAll();
			return Ok(courses);
		}

		[HttpPost("")]
		[JwtAuthorization]
		public IActionResult CreateLecture([FromBody] AddLectureRequestDto newLecture)
		{
			try
			{
				var username = User.FindFirstValue(ClaimTypes.Name);
				var lecture = lectureService.Create(newLecture, username);
				return Ok(lecture);
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
		[JwtAuthorization]
		public IActionResult UpdateLecture(string id, [FromBody] LectureRequestDto newData, [FromHeader] string username)
		{
			try
			{
				var updated = lectureService.Update(new Guid(id), newData);
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
		[JwtAuthorization]
		public IActionResult DeleteLecture(string id, [FromHeader] string username)
		{
			try
			{
				var deleted = lectureService.Delete(new Guid(id));
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
