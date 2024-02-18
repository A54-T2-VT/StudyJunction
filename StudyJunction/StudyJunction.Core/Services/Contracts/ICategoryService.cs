using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.ViewModels.Categories;

namespace StudyJunction.Core.Services.Contracts
{
    public interface ICategoryService
	{
		Task<IEnumerable<CategoryResponseDTO>> GetAll();
		Task<AddCategoryViewModel> GetAllParentCategoriesForAddingCategory();
		Task<AddCategoryViewModel> GetAllParentChildCategoriesForSelectingCategory();

        Task CreateCategoryFromViewModel(AddCategoryViewModel model);
        Task<CategoryResponseDTO> GetById(Guid id);
		Task<CategoryResponseDTO> GetByName(string name);
		Task<CategoryResponseDTO> Create(AddCategoryRequestDto newCategory);
		Task<CategoryResponseDTO> CreateSubCategory(AddCategoryRequestDto newCategory, Guid parentId);
		Task<CategoryResponseDTO> Update(Guid id, CategoryRequestDto updatedCategory);
		Task<CategoryResponseDTO> Delete(Guid id);

		
	}
}
