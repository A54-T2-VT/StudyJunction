using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
	public interface ICategoryService
	{
		IEnumerable<CategoryResponseDTO> GetAll();
		CategoryResponseDTO GetById(Guid id);
		CategoryResponseDTO GetByName(string name);
		CategoryResponseDTO Create(AddCategoryRequestDto newCategory, string username);
		CategoryResponseDTO CreateSubCategory(AddCategoryRequestDto newCategory, string username);
		CategoryResponseDTO Update(Guid id, CategoryRequestDto updatedCategory, string username);
		CategoryResponseDTO Delete(Guid id, string username);

		
	}
}
