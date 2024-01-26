using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<CategoryDb>> GetAll();
		Task<CategoryDb> GetById(Guid id);
		Task<CategoryDb> GetByName(string name);
		Task<CategoryDb> Create(CategoryDb newCategory);
		Task<CategoryDb> Update(Guid id, CategoryDb updatedCategory);
		Task<CategoryDb> Delete(Guid id);
	}
}
