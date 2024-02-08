using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Helpers;
using StudyJunction.Core.RequestDTOs.Course;
using StudyJunction.Core.Services;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Web.CustomAttributes;
using System.Security.Claims;

namespace StudyJunction.Web.Controllers.API
{
    [Route("api/courses")]
	[ApiController]
	public class CoursesApiController : ControllerBase
	{
		private readonly IUserService userService;
		private readonly ICourseService courseService;
        public CoursesApiController(ICourseService _courseService, IUserService _userService)
        {
            courseService = _courseService;
			userService = _userService;
        }

		[HttpGet("{id}")]
		public IActionResult GetById(string id)
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
        [JwtAuthorization(ClearedRoles = new string[] { RolesConstants.Teacher, RolesConstants.Admin, RolesConstants.God })]
        public IActionResult CreateCourse([FromBody] AddCourseRequestDto newCourse)
		{
			try
			{
				var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
				var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

				var course = courseService.Create(newCourse, username);
				return Ok(course);
			}
			catch (UnauthorizedUserException e)
			{
				return Unauthorized(e.Message);
			}
			catch (NameDuplicationException ex)
			{
				return Conflict(ex.Message);
			}
		}

        [HttpPut("{id}")]
        public async Task<IActionResult> AddThumbnailToCourse(string id, IFormFile image)
        {
            try
            {
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var userId = JwtHelper.GetNameIdentifierClaimFromJwt(jwtBearer);

                var result = await courseService.AddThumbnailAsync(id, image, userId);

                return Ok(result);
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (CloudinaryFileUploadException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [JwtAuthorization(ClearedRoles = new string[] { RolesConstants.Teacher, RolesConstants.Admin, RolesConstants.God })]
        public IActionResult UpdateCourse(string id, [FromBody] CourseRequestDto newData) 
		{
			try
			{
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

                var updated = courseService.Update(new Guid(id), newData, username);
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
        [JwtAuthorization(ClearedRoles = new string[] { RolesConstants.Teacher, RolesConstants.Admin, RolesConstants.God })]
        public IActionResult DeleteCourse(string id)
		{
			try
			{
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

                var deleted = courseService.Delete(new Guid(id), username);
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
