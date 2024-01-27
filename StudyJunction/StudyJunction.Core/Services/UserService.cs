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

        public UserResponseDTO Login(LoginUserRequestDto loginUserDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseDTO> Register(RegisterUserRequestDto newUser)
        {
            var userDb = mapper.Map<UserDb>(newUser);

            var result = await userManager.CreateAsync(userDb, newUser.Password);
            //await CreateRoles();

            var user = await userManager.FindByEmailAsync(newUser.Email);
            await userManager.AddToRolesAsync(user, new string[] { RolesConstants.Student });

            if (!result.Succeeded)
            {
                throw new NotImplementedException();
            }
            var responseUser = userRepository.GetByEmailAsync(newUser.Email);

            return mapper.Map<UserResponseDTO>(responseUser.Result);
            
        }

        public UserResponseDTO Update(UserRequestDto updatedUser, string username)
        {
            throw new NotImplementedException();
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
                expires: DateTime.Now.AddHours(1),
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
