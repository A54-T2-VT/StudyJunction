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
			//this.CreateMap<AddCourseRequestDto, CourseDb>();

			CreateMap<AddCourseRequestDto, CourseDb>()
			.ForMember(dest => dest.CategoryId, opt => opt.MapFrom
            ((src, dest, destMember) => destMember));

			this.CreateMap<CourseDb, CourseResponseDTO>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.CreatedBy.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            this.CreateMap<CourseRequestDto, CourseDb>();
        }

		private static string ExtractUserName(string email)
        {
            // Add null check or validation if needed
            int atIndex = email.IndexOf('@');
            return atIndex != -1 ? email.Substring(0, atIndex) : email;
        }
	}
}
