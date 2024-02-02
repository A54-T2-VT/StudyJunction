using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
    public interface ICategoryService
	{
		IEnumerable<CategoryResponseDTO> GetAll();
		CategoryResponseDTO GetById(Guid id);
		CategoryResponseDTO GetByName(string name);
		CategoryResponseDTO Create(AddCategoryRequestDto newCategory);
		CategoryResponseDTO CreateSubCategory(AddCategoryRequestDto newCategory, Guid parentId);
		CategoryResponseDTO Update(Guid id, CategoryRequestDto updatedCategory);
		CategoryResponseDTO Delete(Guid id);

		
	}
}
