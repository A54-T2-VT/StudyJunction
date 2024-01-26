using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
	public interface IUserService
	{
		IEnumerable<UserResponseDTO> GetAll();
		UserResponseDTO GetById(string id);
		UserResponseDTO GetByUsername(string username);
		UserResponseDTO Create(AddUserRequestDto newUser);
		UserResponseDTO Update(UserRequestDto updatedUser);
		UserResponseDTO Delete(string id);
	}
}
