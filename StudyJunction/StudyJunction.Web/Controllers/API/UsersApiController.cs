using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.Services;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Web.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersApiController : ControllerBase
	{
		private IUserService userService;
        public UsersApiController(IUserService _userService)
        {
			userService = _userService;
        }

        [HttpGet("{id}")]
		public IActionResult Get(string id)
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

		[HttpGet("{username}")]
		public IActionResult GetByUsername(string username)
		{
			try
			{
				var user = userService.GetByUsername(username);
				return Ok(user);
			}
			catch (EntityNotFoundException e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("")]
		public IActionResult GetUsers()
		{
			var users = userService.GetAll();
			return Ok(users);
		}

		[HttpPost("")]
		public IActionResult CreateUser([FromBody] AddUserRequestDto newUser)
		{
			try
			{
				var user = userService.Create(newUser);
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
		}

		[HttpPut("{id}")]
		public IActionResult UpdateUser(string id, [FromBody] UserRequestDto newData)
		{
			try
			{
				var updated = userService.Update(/*TODO: add id of userToUpdate*/newData);
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
		public IActionResult DeleteUser(string id)
		{
			try
			{
				var deleted = userService.Delete(id);
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
