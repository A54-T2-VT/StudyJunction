using AutoMapper;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.ViewModels.Categories;

namespace StudyJunction.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
		private readonly IMapper mapper;

		public CategoryService( ICategoryRepository _categoryRepository,
            IMapper _mapper)
        {
            categoryRepository = _categoryRepository;
            mapper = _mapper;
		}

        public async Task<CategoryResponseDTO> Create(AddCategoryRequestDto newCategory)
		{
			if (await categoryRepository.CategoryNameExists(newCategory.Name))
			{
				throw new NameDuplicationException(
					String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCategory.Name));
			}

            var map = mapper.Map<CategoryDb>(newCategory);

			var created = await categoryRepository.CreateAsync(map);

            return mapper.Map<CategoryResponseDTO>(created);
		}

		public async Task<CategoryResponseDTO> CreateSubCategory(AddCategoryRequestDto newCategory, Guid parentId)
        {
			if (await categoryRepository.CategoryNameExists(newCategory.Name))
			{
				throw new NameDuplicationException(
					String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCategory.Name));
			}

			var parent = await categoryRepository.GetByIdAsync(parentId);
            var sub = mapper.Map<CategoryDb>(newCategory);

            return mapper.Map<CategoryResponseDTO>(await categoryRepository.AddSubCategory(parent, sub));
		}

        public async Task CreateCategoryFromViewModel(AddCategoryViewModel model)
        {
            if(model.ParentCategory == "None")//"None" is when tha category beeing created will be parent category
            {
                var categoryDb = new CategoryDb();
                categoryDb.Name = model.Name;

                _ = await categoryRepository.CreateAsync(categoryDb);

                return;
            }

            var parent = await categoryRepository.GetByNameAsync(model.ParentCategory);
            var child = new CategoryDb();
            child.Name = model.Name;

            _ = await categoryRepository.AddSubCategory(parent, child);
            
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetAll()
        {
            var categories = await categoryRepository.GetAllAsync();

            return categories
                .Select(cat => mapper.Map<CategoryResponseDTO>(cat))
                .ToList();
        }

        public async Task<AddCategoryViewModel> GetAllParentCategoriesForAddingCategory()
        {
            var categoreisDb = await categoryRepository.GetAllParentCategories();

            var parentCategories = new Dictionary<string, List<string>>();//parent, child

            foreach(var categoryDb in categoreisDb)
            {
                if(categoryDb.ParentCategory is null)
                {
                    parentCategories.Add(categoryDb.Name, new List<string>());
                    continue;
                }

                parentCategories[categoryDb.ParentCategory.Name].Add(categoryDb.Name);
            }

            var model = new AddCategoryViewModel();
            model.ParentChildCategories = parentCategories;

            return model;

        }

        public async Task<CategoryResponseDTO> GetById(Guid id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            return mapper.Map<CategoryResponseDTO>(category);
        }

        public async Task<CategoryResponseDTO> GetByName(string name)
        {
			var category = await categoryRepository.GetByNameAsync(name);

			return mapper.Map<CategoryResponseDTO>(category);
		}

		public async Task<CategoryResponseDTO> Update(Guid id, CategoryRequestDto updatedCategory)
        {
            var updated = mapper.Map<CategoryDb>(updatedCategory);

            if(await categoryRepository.CategoryNameExists(updatedCategory.Name))
            {
                throw new NameDuplicationException(
                    String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, updatedCategory.Name));
            }

            return mapper.Map<CategoryResponseDTO>(await categoryRepository.UpdateAsync(id, updated));
		}

		public async Task<CategoryResponseDTO> Delete(Guid id)
		{
			return mapper.Map<CategoryResponseDTO>(await categoryRepository.DeleteAsync(id));
		}
	}
}
