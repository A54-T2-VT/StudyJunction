using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.Services;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using System.Security.Claims;

namespace StudyJunction.Web.Controllers.API
{
	[Route("api/users")]
	[ApiController]
	[Authorize]
	public class UsersApiController : ControllerBase
	{
		private IUserService userService;
        public UsersApiController(IUserService _userService)
        {
			userService = _userService;
        }

        [HttpGet("{id}")]
		public IActionResult GetById(string id)
		{
			try
			{
				var user = userService.GetById(id);
				return Ok(user);
			}
			catch (EntityNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//[HttpGet("{username}")]
		//public IActionResult GetByUsername(string username)
		//{
		//	try
		//	{
		//		var user = userService.GetByUsername(username);
		//		return Ok(user);
		//	}
		//	catch (EntityNotFoundException e)
		//	{
		//		return BadRequest(e.Message);
		//	}
		//}

		[HttpGet("")]
		public IActionResult GetUsers()
		{
			var users = userService.GetAll();
			return Ok(users);
		}

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginUserRequestDto loginDto)
        {
            try
            {
                var JWT = userService.Login(loginDto).Result;
                return Ok(JWT);
            }
            catch (UnauthorizedUserException e)
            {
                return Unauthorized(e.Message);
            }
            catch (NameDuplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
		[AllowAnonymous]
		public IActionResult Register([FromBody] RegisterUserRequestDto newUser)
		{
			try
			{
				var user = userService.Register(newUser).Result;
				return Ok(user);
			}
			catch (UnauthorizedUserException e)
			{
				return Unauthorized(e.Message);
			}
			catch (NameDuplicationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("")]
		public IActionResult Update([FromBody] UserRequestDto newData/*, [FromHeader] string authorization*/)
		{
			try
			{
				var username = User.FindFirstValue(ClaimTypes.Name);
				var updated = userService.Update(newData, username);
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
		public IActionResult DeleteUser(string id, [FromHeader] string username)
		{
			try
			{
				var deleted = userService.Delete(id, username);
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
