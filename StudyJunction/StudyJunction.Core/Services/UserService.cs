using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Core.Services
{
    public class UserService : IUserService
    {
		private readonly IUserRepository userRepository;
		public UserService(IUserRepository _userRepository)
		{
			userRepository = _userRepository;
		}
		public UserResponseDTO Create(RegisterUserRequestDto newUser, string username)
        {
            throw new NotImplementedException();
        }

        public UserResponseDTO Delete(string id, string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserResponseDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserResponseDTO GetById(string id)
        {
            throw new NotImplementedException();
        }

        public UserResponseDTO GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public UserResponseDTO Update(UserRequestDto updatedUser, string username)
        {
            throw new NotImplementedException();
        }
    }
}
