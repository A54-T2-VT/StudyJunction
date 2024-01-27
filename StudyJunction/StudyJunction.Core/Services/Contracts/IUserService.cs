using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
	public interface IUserService
	{
		IEnumerable<UserResponseDTO> GetAll();
		UserResponseDTO GetById(string id);
		UserResponseDTO GetByUsername(string username);
        Task<UserResponseDTO> Register(RegisterUserRequestDto newUser);
        Task<string> Login(LoginUserRequestDto loginUserDto);
		UserResponseDTO Update(UserRequestDto updatedUser, string username);
		UserResponseDTO Delete(string id, string username);
	}
}
