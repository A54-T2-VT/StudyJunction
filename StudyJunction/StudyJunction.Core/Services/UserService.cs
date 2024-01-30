using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyJunction.Core.RequestDTOs;
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

            //var result = userManager.DeleteAsync(userToDelete).Result;

            //if (!result.Succeeded)
            //{
            //    throw new NotImplementedException();
            //}

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

            var result = await signInManager.PasswordSignInAsync(user, loginUserDto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new NotImplementedException();
            }


            return CreateToken(user);
        }

        public async Task<UserResponseDTO> Register(RegisterUserRequestDto newUser)
        {
            var userDb = mapper.Map<UserDb>(newUser);

            var result = await userManager.CreateAsync(userDb, newUser.Password);

            var user = await userManager.FindByEmailAsync(newUser.Email);
            await userManager.AddToRolesAsync(user, new string[] { RolesConstants.Student });

            if (!result.Succeeded)
            {
                throw new NotImplementedException();
            }
            var responseUser = userRepository.GetByEmailAsync(newUser.Email);

            return mapper.Map<UserResponseDTO>(responseUser.Result);
            
        }

        public UserResponseDTO Update(UpdateUserDataRequestDto updatedUser, string username)
        {
            
            var toUpdate = userManager.FindByNameAsync(username).Result;
            


            toUpdate.FirstName = updatedUser.Firstname;
            toUpdate.LastName = updatedUser.Lastname;
            //userManager.ChangePasswordAsync(toUpdate, updatedUser.Password);

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

        private string CreateToken(UserDb user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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

        private async Task CreateRoles()
        {
            await roleManager.CreateAsync(new IdentityRole(RolesConstants.God));
            await roleManager.CreateAsync(new IdentityRole(RolesConstants.Admin));
            await roleManager.CreateAsync(new IdentityRole(RolesConstants.Teacher));
            await roleManager.CreateAsync(new IdentityRole(RolesConstants.Student));
        }
    }
}
