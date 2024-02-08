﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Core.RequestDTOs.Category;

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

        public async Task<CategoryResponseDTO> Create(AddCategoryRequestDto newCategory)
		{
			if (categoryRepository.CategoryNameExists(newCategory.Name))
			{
				throw new NameDuplicationException(
					String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCategory.Name));
			}

			var created = await categoryRepository.CreateAsync(mapper.Map<CategoryDb>(newCategory));

            return mapper.Map<CategoryResponseDTO>(created);
		}

		public async Task<CategoryResponseDTO> CreateSubCategory(AddCategoryRequestDto newCategory, Guid parentId)
        {
			if (categoryRepository.CategoryNameExists(newCategory.Name))
			{
				throw new NameDuplicationException(
					String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCategory.Name));
			}

			var parent = await categoryRepository.GetByIdAsync(parentId);
            var sub = mapper.Map<CategoryDb>(newCategory);

            return mapper.Map<CategoryResponseDTO>(await categoryRepository.AddSubCategory(parent, sub));
		}

        public async Task<IEnumerable<CategoryResponseDTO>> GetAll()
        {
            var categories = await categoryRepository.GetAllAsync();

            return categories
                .Select(cat => mapper.Map<CategoryResponseDTO>(cat))
                .ToList();
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

            if(categoryRepository.CategoryNameExists(updatedCategory.Name))
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
