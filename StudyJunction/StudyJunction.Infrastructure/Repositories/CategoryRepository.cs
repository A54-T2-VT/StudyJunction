using Microsoft.EntityFrameworkCore;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SJDbContext context;
        public CategoryRepository(SJDbContext _context)
        {
            context = _context;
        }

        public async Task<CategoryDb> CreateAsync(CategoryDb newCategory)
        {
            await context.Categories.AddAsync(newCategory);
            await context.SaveChangesAsync();
            return newCategory;
        }

        public async Task<CategoryDb> DeleteAsync(Guid id)
        {
            var categoryToDelete = await context.Categories.FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new EntityNotFoundException
                (String.Format(ExceptionMessages.CATEGORY_WITH_ID_NOT_FOUND_MESSAGE, id));

            context.Categories.Remove(categoryToDelete);
            context.SaveChanges();
            return categoryToDelete;
        }

        public async Task<IEnumerable<CategoryDb>> GetAllAsync()
        {
            var categories = await context.Categories.ToListAsync();

            return categories;
        }

        public async Task<IEnumerable<CategoryDb>> GetAllParentCategories()
        {
            var parentCategories = await context.Categories.Where(c => c.ParentCategoryId == null).ToListAsync();

            return parentCategories;
        }

        public async Task<CategoryDb> GetByIdAsync(Guid id)
        {
            var c = await context.Categories.FirstOrDefaultAsync(c => c.Id.Equals(id))
                ?? throw new EntityNotFoundException
                (String.Format(ExceptionMessages.CATEGORY_WITH_ID_NOT_FOUND_MESSAGE, id));

            return c;
        }

        public async Task<CategoryDb> GetByNameAsync(string name)
        {
			var c = await context.Categories.FirstOrDefaultAsync(c => c.Name.Equals(name))
				?? throw new EntityNotFoundException
                (String.Format(ExceptionMessages.CATEGORY_WITH_NAME_NOT_FOUND_MESSAGE, name));
            
			return c;
		}

        public async Task<CategoryDb> UpdateAsync(Guid id, CategoryDb updatedCategory)
        {
            var toUpdate = await context.Categories.FirstOrDefaultAsync(c => c.Id.Equals(id))
                ?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.CATEGORY_WITH_ID_NOT_FOUND_MESSAGE, id));

            toUpdate.Name = updatedCategory.Name;
            toUpdate.ParentCategory = updatedCategory.ParentCategory;
            toUpdate.ParentCategoryId = updatedCategory.ParentCategoryId;

            await context.SaveChangesAsync();
            return toUpdate;
		}

		public async Task<CategoryDb> AddSubCategory(CategoryDb parent, CategoryDb subCategory)
		{
			subCategory.ParentCategory = parent;
            subCategory.ParentCategoryId = parent.Id;
			await context.Categories.AddAsync(subCategory);
			await context.SaveChangesAsync();
            return subCategory;
		}

		public async Task<bool> CategoryNameExists(string name)
		{
            return await context.Categories.AnyAsync(x => x.Name == name);
		}
	}
}
