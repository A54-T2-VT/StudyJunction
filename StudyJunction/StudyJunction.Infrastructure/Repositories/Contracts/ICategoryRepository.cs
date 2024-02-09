using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<CategoryDb>> GetAllAsync();
		Task<CategoryDb> GetByIdAsync(Guid id);
		Task<CategoryDb> GetByNameAsync(string name);
		Task<CategoryDb> CreateAsync(CategoryDb newCategory);
		Task<CategoryDb> AddSubCategory(CategoryDb parent, CategoryDb subCategory);
		Task<CategoryDb> UpdateAsync(Guid id, CategoryDb updatedCategory);
		Task<CategoryDb> DeleteAsync(Guid id);
		Task<bool> CategoryNameExists(string name);
	}
}
