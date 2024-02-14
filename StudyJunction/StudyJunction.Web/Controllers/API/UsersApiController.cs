using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Helpers;
using StudyJunction.Core.RequestDTOs.User;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using StudyJunction.Web.CustomAttributes;
using System.Data;
using System.Security.Claims;

namespace StudyJunction.Web.Controllers.API
{
    [Route("api/users")]
	[ApiController]
	public class UsersApiController : ControllerBase
	{
		private IUserService userService;

        public UsersApiController(IUserService _userService)
        {
			userService = _userService;
        }

        [HttpGet("find")]
        [JwtAuthorization]
        public async Task<IActionResult> FindUser(string searchTerm)
		{
			try
			{
				UserResponseDTO user;

				// Check if the searchTerm is a valid email
				if (searchTerm.Contains("@"))
				{
					user =  await userService.GetByEmail(searchTerm);
				}
				// Check if the searchTerm is a valid ID
				else if (Guid.TryParse(searchTerm, out var userId))
				{
					user =  await userService.GetById(userId.ToString());
				}
				// Assume it's a username
				else
				{
					user =  await userService.GetByUsername(searchTerm);
				}

				if (user == null)
				{
					return NotFound("User not found");
				}
				
				return Ok(user);
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
        }

		[HttpGet]
        [JwtAuthorization(ClearedRoles = new string[] { RolesConstants.Teacher})]
        public async Task<IActionResult> GetUsers()
		{
			var users = await userService.GetAll();
			return Ok(users);
		}

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto loginDto)
        {
            try
            {
                var JWT = await userService.Login(loginDto);
                return Ok(JWT);
            }
            catch (UnauthorizedUserException e)
            {
                return Unauthorized(e.Message);
            }
            catch (NameDuplicationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto newUser)
		{
			try
			{
				var user = await userService.Register(newUser);
				return Ok(user);
			}
			catch (UnauthorizedUserException e)
			{
				return Unauthorized(e.Message);
			}
			catch (NameDuplicationException ex)
			{
				return Conflict(ex.Message);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("increaseRole/{targetUserId}")]
		[JwtAuthorization(ClearedRoles = new string[] { RolesConstants.Admin, RolesConstants.God})]
		public async Task<IActionResult> IncreaseRole(string targetUserId)
		{
			try
			{
				var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
				var userTokenId = JwtHelper.GetNameIdentifierClaimFromJwt(jwtBearer);

				if(userTokenId == targetUserId)
				{
					throw new Exception("User can not increase his own role.");
				}

				string newRole = await userService.IncreaseRole(targetUserId);

				return Ok(newRole);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
        }

        [HttpPut("decreaseRole/{targetUserId}")]
        [JwtAuthorization(ClearedRoles = new string[] { RolesConstants.Admin, RolesConstants.God})]
        public async Task<IActionResult> DecreaseRole(string targetUserId)
        {
            try
            {
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var userTokenId = JwtHelper.GetNameIdentifierClaimFromJwt(jwtBearer);

                if (userTokenId == targetUserId)
                {
                    throw new Exception("User can not increase his own role.");
                }

                string newRole = await userService.DecreaseRole(targetUserId);

                return Ok(newRole);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("")]
        [JwtAuthorization]
        public async Task<IActionResult> Update([FromBody] UpdateUserDataRequestDto newData)
		{
			try
			{
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
				var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

				var updated = await userService.Update(newData, username);
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
            catch (NotImplementedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("password")]
        [JwtAuthorization]
        public async Task<IActionResult> Update([FromBody] UpdateUserPasswordRequestDto passData)
        {
            try
            {
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

                var updated = await userService.Update(passData, username);
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
			catch(NotImplementedException ex)
			{
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        [JwtAuthorization]
        public async Task<IActionResult> DeleteUser(string id)
		{
			try
			{
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);

                var deleted = await userService.Delete(id, username);
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
