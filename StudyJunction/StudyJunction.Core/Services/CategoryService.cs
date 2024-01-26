using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Repositories.Contracts;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUserRepository usersRepository;
        private readonly ICategoryRepository categoryRepository;
        public CategoryService(IUserRepository _usersRepository, ICategoryRepository _categoryRepository)
        {
            usersRepository = _usersRepository;
            categoryRepository = _categoryRepository;
        }

        public CategoryResponseDTO Create(AddCategoryRequestDto newCategory, string username)
        {
			var user = usersRepository.GetUser(username).Result;
			// TODO: UserDb hasnt got property IsAdmin
			//if(!user.IsAdmin) 
			//{
			//    throw new UnauthorizedUserException
			//        (String.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
			//}
            //categoryRepository.Create()

            throw new NotImplementedException();
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
