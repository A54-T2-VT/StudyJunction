using StudyJunction.Core.RequestDTOs.User;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
    public interface IUserService
	{
		Task<IEnumerable<UserResponseDTO>> GetAll();
		Task<UserResponseDTO> GetById(string id);
		Task<UserResponseDTO> GetByUsername(string username);
		Task<UserResponseDTO> GetByEmail(string email);
        Task<UserResponseDTO> Register(RegisterUserRequestDto newUser);
        Task<string> Login(LoginUserRequestDto loginUserDto);
		Task<UserResponseDTO> Update(UpdateUserDataRequestDto updatedUser, string username);
        Task<UserResponseDTO> Update(UpdateUserPasswordRequestDto passData, string username);
		Task<string> IncreaseRole(string targetUserId);
        Task<string> DecreaseRole(string targetUserId);
        Task<UserResponseDTO> Delete(string id, string username);
	}
}
