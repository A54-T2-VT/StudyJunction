using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<CategoryDb> Create(CategoryDb newCategory)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDb> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDb>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDb> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDb> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDb> Update(Guid id, CategoryDb updatedCategory)
        {
            throw new NotImplementedException();
        }
    }
}
