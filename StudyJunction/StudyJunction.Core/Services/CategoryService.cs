using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Repositories.Contracts;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AutoMapper;

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

        public CategoryResponseDTO Create(AddCategoryRequestDto newCategory, string username)
		{
            //backup plan, besides [Authorize(Role)]
            //var user = userManager.FindByNameAsync(username).Result;

            //if (!userManager.IsInRoleAsync(user, RolesConstants.Admin).Result)
            //{
            //	throw new UnauthorizedUserException(String.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
            //}

            var created = categoryRepository.CreateAsync(mapper.Map<CategoryDb>(newCategory)).Result;

            return mapper.Map<CategoryResponseDTO>(created);
		}

		//TODO: add way to access the parent category here
		public CategoryResponseDTO CreateSubCategory(AddCategoryRequestDto newCategory, string username)
        {
			throw new NotImplementedException();
		}

        public IEnumerable<CategoryResponseDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CategoryResponseDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public CategoryResponseDTO GetByName(string name)
        {
            throw new NotImplementedException();
        }

		public CategoryResponseDTO Update(Guid id, CategoryRequestDto updatedCategory, string username)
		{
			throw new NotImplementedException();
		}

		public CategoryResponseDTO Delete(Guid id, string username)
		{
			throw new NotImplementedException();
		}
	}
}
