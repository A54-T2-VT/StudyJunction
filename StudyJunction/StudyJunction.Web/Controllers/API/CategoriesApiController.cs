using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Helpers;
using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Web.CustomAttributes;
using System.Security.Claims;

namespace StudyJunction.Web.Controllers.API
{
    [Route("api/categories")]
	[ApiController]
	public class CategoriesApiController : ControllerBase
	{
		private ICategoryService categoryService;
		public CategoriesApiController(ICategoryService _categoryService)
		{
			categoryService = _categoryService;
		}

		[HttpGet("find")]
		public IActionResult FindCategory(string searchTerm)
		{
			try
			{
				CategoryResponseDTO category;

				if(Guid.TryParse(searchTerm, out Guid id)) 
				{
                    category = categoryService.GetById(id);

				}
                else
                {
					category = categoryService.GetByName(searchTerm);
                }


				return Ok(category);
			}
			catch (EntityNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("")]
		public IActionResult GetAll()
		{
			return Ok(categoryService.GetAll());
		}

		[HttpPost("")]
        [JwtAuthorization(ClearedRoles = new string[] { RolesConstants.Admin, RolesConstants.God })]
        public IActionResult CreateCategory([FromBody] AddCategoryRequestDto dto)
		{
			try
			{
				var newCategory = categoryService.Create(dto);
				return Ok(newCategory);
			}
			catch (UnauthorizedUserException ex)
			{
				return Unauthorized(ex.Message);
			}
			catch (NameDuplicationException ex)
			{
				return Conflict(ex.Message);
			}
		}
		[HttpPost("{id}")]
        [JwtAuthorization(ClearedRoles = new string[] { RolesConstants.Admin, RolesConstants.God })]
        public IActionResult CreateSubCategory(string id, [FromBody] AddCategoryRequestDto dto)
		{
			try
			{
                var jwtBearer = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var username = JwtHelper.GetNameClaimFromJwt(jwtBearer);
                var newSubCategory = categoryService.CreateSubCategory(dto, new Guid(id));
				return Ok(newSubCategory);
			}
			catch (UnauthorizedUserException ex)
			{
				return Unauthorized(ex.Message);
			}
			catch (NameDuplicationException ex)
			{
				return Conflict(ex.Message);
			}
		}

		[HttpPut("{id}")]
        [JwtAuthorization(ClearedRoles = new string[] { RolesConstants.Admin, RolesConstants.God })]
        public IActionResult UpdateCategory(string id, [FromBody] CategoryRequestDto newData)
		{
			try
			{
				var updated = categoryService.Update(new Guid(id), newData);
				return Ok(updated);
			}
			catch(UnauthorizedUserException ex)
			{
				return BadRequest(ex.Message);
			}
			catch(EntityNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			catch(NameDuplicationException ex)
			{
				return Conflict(ex.Message);
			}
		}
		[HttpDelete("{id}")]
		[JwtAuthorization(ClearedRoles = new string[] {RolesConstants.Admin, RolesConstants.God })]
		public IActionResult DeleteCategory(string id)
		{
			try
			{
				var deleted = categoryService.Delete(new Guid(id));
				return Ok(deleted);
			}
			catch(UnauthorizedUserException ex)
			{
				return BadRequest(ex.Message);
			}
			catch(EntityNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
