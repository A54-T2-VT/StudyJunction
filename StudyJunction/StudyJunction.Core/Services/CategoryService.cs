using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Constants;

namespace StudyJunction.Core.Services
{
	public class CategoryService : ICategoryService
    {
        private readonly IUserRepository usersRepository;
        private readonly ICategoryRepository categoryRepository;
		private readonly IMapper mapper;
		private readonly UserManager<UserDb> userManager;

		public CategoryService(IUserRepository _usersRepository, ICategoryRepository _categoryRepository,
            IMapper _mapper, UserManager<UserDb> _userManager)
        {
            usersRepository = _usersRepository;
            categoryRepository = _categoryRepository;
            mapper = _mapper;
			userManager = _userManager;
		}

        public CategoryResponseDTO Create(AddCategoryRequestDto newCategory)
		{
			if (categoryRepository.CategoryNameExists(newCategory.Name))
			{
				throw new NameDuplicationException(
					String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCategory.Name));
			}

			var created = categoryRepository.CreateAsync(mapper.Map<CategoryDb>(newCategory)).Result;

            return mapper.Map<CategoryResponseDTO>(created);
		}

		public CategoryResponseDTO CreateSubCategory(AddCategoryRequestDto newCategory, Guid parentId)
        {
			if (categoryRepository.CategoryNameExists(newCategory.Name))
			{
				throw new NameDuplicationException(
					String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCategory.Name));
			}

			var parent = categoryRepository.GetByIdAsync(parentId).Result;
            var sub = mapper.Map<CategoryDb>(newCategory);

            return mapper.Map<CategoryResponseDTO>(categoryRepository.AddSubCategory(parent, sub).Result);
		}

        public IEnumerable<CategoryResponseDTO> GetAll()
        {
            return categoryRepository.GetAllAsync().Result
                .Select(cat => mapper.Map<CategoryResponseDTO>(cat))
                .ToList();
        }

        public CategoryResponseDTO GetById(Guid id)
        {
            var category = categoryRepository.GetByIdAsync(id).Result;

            return mapper.Map<CategoryResponseDTO>(category);
        }

        public CategoryResponseDTO GetByName(string name)
        {
			var category = categoryRepository.GetByNameAsync(name).Result;

			return mapper.Map<CategoryResponseDTO>(category);
		}

		public CategoryResponseDTO Update(Guid id, CategoryRequestDto updatedCategory)
        {
            var updated = mapper.Map<CategoryDb>(updatedCategory);

            if(categoryRepository.CategoryNameExists(updatedCategory.Name))
            {
                throw new NameDuplicationException(
                    String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, updatedCategory.Name));
            }

            return mapper.Map<CategoryResponseDTO>(categoryRepository.UpdateAsync(id, updated).Result);
		}

		public CategoryResponseDTO Delete(Guid id)
		{
			return mapper.Map<CategoryResponseDTO>(categoryRepository.DeleteAsync(id).Result);
		}
	}
}
