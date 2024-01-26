using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;

namespace StudyJunction.Core.Services
{
    public class UserService : IUserService
    {
        public UserResponseDTO Create(AddUserRequestDto newUser)
        {
            throw new NotImplementedException();
        }

        public UserResponseDTO Delete(string id)
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

        public UserResponseDTO Update(UserRequestDto updatedUser)
        {
            throw new NotImplementedException();
        }
    }
}
