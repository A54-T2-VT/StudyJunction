using AutoMapper;
using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Core.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<UserDb, UserResponseDTO>();
            this.CreateMap<RegisterUserRequestDto, UserDb>()
                .ForMember(d => d.UserName, p => p.MapFrom(s => ExtractUserName(s.Email)));
            this.CreateMap<LoginUserRequestDto, UserDb>();
            this.CreateMap<AddCategoryRequestDto, CategoryDb>();
            this.CreateMap<CategoryDb, CategoryResponseDTO>();
            this.CreateMap<CategoryRequestDto, CategoryDb>();
        }

        private static string ExtractUserName(string email)
        {
            // Add null check or validation if needed
            int atIndex = email.IndexOf('@');
            return atIndex != -1 ? email.Substring(0, atIndex) : email;
        }
    }
}
