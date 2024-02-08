using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
    public interface ICategoryService
	{
		Task<IEnumerable<CategoryResponseDTO>> GetAll();
		Task<CategoryResponseDTO> GetById(Guid id);
		Task<CategoryResponseDTO> GetByName(string name);
		Task<CategoryResponseDTO> Create(AddCategoryRequestDto newCategory);
		Task<CategoryResponseDTO> CreateSubCategory(AddCategoryRequestDto newCategory, Guid parentId);
		Task<CategoryResponseDTO> Update(Guid id, CategoryRequestDto updatedCategory);
		Task<CategoryResponseDTO> Delete(Guid id);

		
	}
}
