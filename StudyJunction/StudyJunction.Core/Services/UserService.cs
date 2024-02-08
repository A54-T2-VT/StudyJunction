using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyJunction.Core.RequestDTOs.User;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudyJunction.Core.Services
{
    public class UserService : IUserService
    {
		private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly UserManager<UserDb> userManager;
        private readonly SignInManager<UserDb> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(IUserRepository _userRepository
            , IConfiguration configuration
            , IMapper mapper
            , UserManager<UserDb> _userManager,
            SignInManager<UserDb> _signInManager,
            RoleManager<IdentityRole> _roleManager)
		{
			userRepository = _userRepository;
            this.configuration = configuration;
            this.mapper = mapper;
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }


        public UserResponseDTO Delete(string id, string username)
        {
            var userToDelete = userManager.FindByIdAsync(id).Result;

            if (!(userToDelete.UserName == username))
            {
                throw new UnauthorizedUserException(string.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
            }


            var user = userRepository.DeleteAsync(id).Result;

            return mapper.Map<UserResponseDTO>(user);
        }

        public IEnumerable<UserResponseDTO> GetAll()
        {
            var result = userRepository.GetAllAsync().Result;

            return result.Select(
                r => mapper.Map<UserResponseDTO>(r))
                .ToList();
        }

        public UserResponseDTO GetById(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            
            if(user is null)
            {
                throw new EntityNotFoundException(string.Format(ExceptionMessages.USER_WITH_ID_NOT_FOUND_MESSAGE, id));
            }

            return mapper.Map<UserResponseDTO>(user);
        }

        public UserResponseDTO GetByUsername(string username)
        {
            var user = userManager.FindByNameAsync(username).Result;

            if (user is null)
            {
                throw new EntityNotFoundException(string.Format(ExceptionMessages.USER_WITH_USERNAME_NOT_FOUND_MESSAGE, username));
            }

            return mapper.Map<UserResponseDTO>(user);
        }

        public UserResponseDTO GetByEmail(string email)
        {
            var user = userManager.FindByEmailAsync(email).Result;

            if (user is null)
            {
                throw new EntityNotFoundException(string.Format(ExceptionMessages.USER_WITH_EMAIL_NOT_FOUND_MESSAGE, email));
            }

            return mapper.Map<UserResponseDTO>(user);
        }

        public async Task<string> Login(LoginUserRequestDto loginUserDto)
        {
            var user = await userManager.FindByEmailAsync(loginUserDto.Email);


            var passwordIsValid = userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!(await passwordIsValid))
            {
                throw new InvalidCredentialsException(ExceptionMessages.INVALID_CREDENTIALS_MESSAGE);
            }


            return await CreateToken(user);
        }

        public async Task<UserResponseDTO> Register(RegisterUserRequestDto newUser)
        {
            var userDb = mapper.Map<UserDb>(newUser);

            var result = await userManager.CreateAsync(userDb, newUser.Password);

            if (!result.Succeeded)
            {
                throw new NameDuplicationException(ExceptionMessages.INVALID_CREDENTIALS_MESSAGE);
            }

            var user = await userManager.FindByEmailAsync(newUser.Email);
            _ = await userManager.AddToRolesAsync(user, new string[] { RolesConstants.Student });

            var responseUser = userRepository.GetByEmailAsync(newUser.Email);

            return mapper.Map<UserResponseDTO>(responseUser.Result);
            
        }

        public UserResponseDTO Update(UpdateUserDataRequestDto updatedUser, string username)
        {
            
            var toUpdate = userManager.FindByNameAsync(username).Result;
            


            toUpdate.FirstName = updatedUser.Firstname;
            toUpdate.LastName = updatedUser.Lastname;

            var result = userManager.UpdateAsync(toUpdate).Result;

            if (!result.Succeeded)
            {
                throw new NotImplementedException();
            }

            return mapper.Map<UserResponseDTO>(toUpdate);
        }

        public UserResponseDTO Update(UpdateUserPasswordRequestDto passData, string username)
        {
            var toUpdate = userManager.FindByNameAsync(username).Result;

            var result = userManager.ChangePasswordAsync(toUpdate, passData.OldPassword, passData.NewPassword).Result;

            if (!result.Succeeded)
            {
                throw new NotImplementedException();
            }

            return mapper.Map<UserResponseDTO>(toUpdate);

        }

        public string IncreaseRole(string targetUserId)
        {
            var user = userManager.FindByIdAsync(targetUserId).Result;

            var userRoles = userManager.GetRolesAsync(user).Result;

            string highestRole = userRoles.Last();

            if (userRoles.Contains(RolesConstants.God) || userRoles.Contains(RolesConstants.Admin)) 
            {
                return highestRole;
            }

            var newHighestRole = AddRole(user, highestRole);
            
          
            return newHighestRole;

        }

        public string DecreaseRole(string targetUserId)
        {
            var user = userManager.FindByIdAsync(targetUserId).Result;

            var userRoles = userManager.GetRolesAsync(user).Result;

            string highestRole = userRoles.Last();

            if(highestRole == RolesConstants.Student || highestRole == RolesConstants.God)
            {
                return highestRole;
            }

            var newHighestRole = RemoveRole(user, highestRole);

            return newHighestRole;
        }

        private async Task<string> CreateToken(UserDb user)
        {
            var highestRole = GetHighestRoleAsync(user);


            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, await highestRole )
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private async Task<string> GetHighestRoleAsync(UserDb user)
        {
            var roles = await userManager.GetRolesAsync(user);

            if (roles.Count == 1)
            {
                return RolesConstants.Student;
            }
            else if (roles.Count == 2)
            {
                return RolesConstants.Teacher;
            }
            else if (roles.Count == 3)
            {
                return RolesConstants.Admin;
            }
            else //roles.Count should be 4
            {
                return RolesConstants.God;
            }


        }

        private async Task CreateRoles()
        {
            await roleManager.CreateAsync(new IdentityRole(RolesConstants.God));
            await roleManager.CreateAsync(new IdentityRole(RolesConstants.Admin));
            await roleManager.CreateAsync(new IdentityRole(RolesConstants.Teacher));
            await roleManager.CreateAsync(new IdentityRole(RolesConstants.Student));
        }

        private string AddRole(UserDb user, string currRole)
        {
            IdentityResult result;

            if(currRole == RolesConstants.Student)
            {
                result = userManager.AddToRoleAsync(user, RolesConstants.Teacher).Result;

                if (!result.Succeeded)
                {
                    throw new Exception("Error when increasing role!!!");
                }

                return RolesConstants.Teacher.ToString();
            }
            //(currRole == RolesConstants.Teacher)
            
            result = userManager.AddToRoleAsync(user, RolesConstants.Admin).Result;

            if (!result.Succeeded)
            {
                throw new Exception("Error when increasing role!!!");
            }
            
            return RolesConstants.Admin.ToString();
        }

        private string RemoveRole(UserDb user, string currRole)
        {
            IdentityResult result;

            if (currRole == RolesConstants.Admin)
            {
                result = userManager.RemoveFromRoleAsync(user, currRole).Result;

                if (!result.Succeeded)
                {
                    throw new Exception("Error when decreasing role!!!");
                }

                return RolesConstants.Teacher.ToString();
            }
            //(currRole == RolesConstants.Teacher)

            result = userManager.RemoveFromRoleAsync(user, currRole).Result;

            if (!result.Succeeded)
            {
                throw new Exception("Error when decreasing role!!!");
            }

            return RolesConstants.Student.ToString();
        }
    }
}
