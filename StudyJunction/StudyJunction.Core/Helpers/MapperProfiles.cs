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
            this.CreateMap<RegisterUserRequestDto, UserDb>();
            this.CreateMap<LoginUserRequestDto, UserDb>();
        }
    }
}
