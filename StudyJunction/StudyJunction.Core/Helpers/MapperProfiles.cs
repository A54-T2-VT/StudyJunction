using AutoMapper;
using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.RequestDTOs.Course;
using StudyJunction.Core.RequestDTOs.Lecture;
using StudyJunction.Core.RequestDTOs.User;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.ViewModels.User;
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Core.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            //Db -> DTO
            this.CreateMap<UserDb, UserResponseDTO>();
            this.CreateMap<CategoryDb, CategoryResponseDTO>();
            this.CreateMap<CourseDb, CourseResponseDTO>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.CreatedBy.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            this.CreateMap<LectureDb, LectureResponseDTO>();

            //User registretion and login
            this.CreateMap<RegisterUserRequestDto, UserDb>()
                .ForMember(d => d.UserName, p => p.MapFrom(s => ExtractUserName(s.Email)));
            this.CreateMap<LoginUserRequestDto, UserDb>();

            //DTO -> Db
            this.CreateMap<AddCategoryRequestDto, CategoryDb>();
            this.CreateMap<AddCourseRequestDto, CourseDb>()
                .ForMember(d => d.CategoryId, p => p.MapFrom(s => new Guid(s.CategoryName)));
            this.CreateMap<AddLectureRequestDto, LectureDb>()
                .ForMember(d => d.CourseId, opt => opt.MapFrom(s => new Guid(s.CourseName)));
            this.CreateMap<CategoryRequestDto, CategoryDb>();
            this.CreateMap<CourseRequestDto, CourseDb>();

            //ViewModel -> DTO
            this.CreateMap<RegisterViewModel, RegisterUserRequestDto>();
            //this.CreateMap<LoginViewModel, LoginUserRequestDto>();
        }

		private static string ExtractUserName(string email)
        {
            // Add null check or validation if needed
            int atIndex = email.IndexOf('@');
            return atIndex != -1 ? email.Substring(0, atIndex) : email;
        }
	}
}
