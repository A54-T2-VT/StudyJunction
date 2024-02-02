using StudyJunction.Core.RequestDTOs.User;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
    public interface IUserService
	{
		IEnumerable<UserResponseDTO> GetAll();
		UserResponseDTO GetById(string id);
		UserResponseDTO GetByUsername(string username);
		UserResponseDTO GetByEmail(string email);
        Task<UserResponseDTO> Register(RegisterUserRequestDto newUser);
        Task<string> Login(LoginUserRequestDto loginUserDto);
		UserResponseDTO Update(UpdateUserDataRequestDto updatedUser, string username);
        UserResponseDTO Update(UpdateUserPasswordRequestDto passData, string username);
        UserResponseDTO Delete(string id, string username);
	}
}
