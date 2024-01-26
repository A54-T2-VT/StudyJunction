using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Exceptions;

namespace StudyJunction.Web.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesApiController : ControllerBase
	{
        private ICategoryService categoryService;
        public CategoriesApiController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

		[HttpGet("{id}")]
		public IActionResult GetById(string id)
		{
			try
			{
				var category = categoryService.GetById(new Guid(id));
				return Ok(category);
			}
			catch(EntityNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{name}")]
		public IActionResult GetByName(string name)
		{
			try
			{
				var category = categoryService.GetByName(name);
				return Ok(category);
			}
			catch(EntityNotFoundException ex)
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
		public IActionResult CreateCategory([FromBody] AddCategoryRequestDto dto, [FromHeader] string username)
		{
			try
			{
				var newCategory = categoryService.Create(dto, username);
				return Ok(newCategory);
			}
			catch (UnauthorizedUserException ex)
			{
				return Unauthorized(ex.Message);
			}
			catch (NameDuplicationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public IActionResult UpdateCategory(string id, [FromBody] CategoryRequestDto newData, [FromHeader] string username)
		{
			try
			{
				var updated = categoryService.Update(new Guid(id), newData, username);
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
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteCategory(string id, [FromHeader] string username)
		{
			try
			{
				var deleted = categoryService.Delete(new Guid(id), username);
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
