using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.Helpers;
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
		public IActionResult CreateLectureWithVideoAndAssignment(IFormCollection form)
		{
			try
			{
				// Access JSON data
				string jsonData = form["jsonData"];

				// Access file
				var file = form.Files["file"];

				CloudinaryService test = new CloudinaryService();

				test.UploadPdfToCloudinary(file);

				;

				//            var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
				//            var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

				//            var lecture = lectureService.Create(newLecture, username);
				//return Ok(lecture);

				throw new NotImplementedException();
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

		[HttpPost("")]
		public IActionResult CreateLecture(AddLectureRequestDto newLecture)
		{
			try 
			{
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

				var result = lectureService.Create(newLecture, username);
				return Ok(result);
            }
			catch(UnauthorizedUserException ex)
			{
				return Unauthorized();
			}
			catch(NameDuplicationException ex)
			{
				return BadRequest();
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> AddAssignmentToLecture(string id, IFormFile assignment)
		{
			try
			{
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var userId = JwtHelper.GetNameIdentifierClaimFromJwt(jwtBearer);

				var result = await lectureService.AddAssignmentAsync(id, assignment, userId);

				return Ok(result);
            }
			catch(UnauthorizedUserException ex) 
			{
				return Unauthorized(ex.Message);
			}
			catch(CloudinaryFileUploadException ex) 
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("{id}")]
		public IActionResult UpdateLecture(string id, [FromBody] LectureRequestDto newData)
		{
			try
			{
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

                var updated = lectureService.Update(new Guid(id), newData, username);
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
		public IActionResult DeleteLecture(string id)
		{
			try
			{
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

                var deleted = lectureService.Delete(new Guid(id), username);
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
