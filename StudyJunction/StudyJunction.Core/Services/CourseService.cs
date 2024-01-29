﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using System.Linq;

namespace StudyJunction.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<UserDb> userManager;
        public CourseService(ICourseRepository _courseRepository, IUserRepository _userRepository,
            IMapper _mapper, ICategoryRepository _categoryRepository, UserManager<UserDb> _userManager)
        {
			courseRepository = _courseRepository;
            userRepository = _userRepository;
            mapper = _mapper;
            categoryRepository = _categoryRepository;
            userManager = _userManager;
		}
		
        public CourseResponseDTO Create(AddCourseRequestDto newCourse, string username)
        {
            if(courseRepository.CourseTitleExists(newCourse.Title))
            {
                throw new NameDuplicationException(
                    String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCourse.Title));
            }

            //its mapped this way because of the needed conversion of string Category
            //to CategoryDb Category & Guid CategoryId
			var category = categoryRepository.GetByNameAsync(newCourse.Category).Result;
            var categoryId = category.Id;
            var creatorUser = userManager.FindByNameAsync(username).Result;

			// Map AddCourseRequestDto to CourseDb
			var courseDb = new CourseDb
			{
				Title = newCourse.Title,
				Description = newCourse.Description,
				CategoryId = categoryId,
				Category = category,
                CreatorId = creatorUser.UserName,
                CreatedBy = creatorUser
			};

			return mapper.Map<CourseResponseDTO>(courseRepository.CreateAsync(courseDb).Result);
        }

        public ICollection<CourseResponseDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CourseResponseDTO GetCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public CourseResponseDTO GetCourse(string title)
        {
            throw new NotImplementedException();
        }

        public CourseResponseDTO Update(Guid toUpdate, CourseRequestDto newData, string username)
        {
			if (courseRepository.CourseTitleExists(newData.Title))
			{
				throw new NameDuplicationException(
					String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newData.Title));
			}

			var user = userManager.FindByNameAsync(username).Result;
            
            //TODO: if the user is a teacher, check if the course is created by them (not optimal at the moment)
			if (userManager.IsInRoleAsync(user, RolesConstants.Teacher).Result 
                && !userRepository.HasCreatedCourse(user, newData.Title))
            {
                throw new UnauthorizedUserException(
                    String.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
            }

            var newCourse = mapper.Map<CourseDb>(newData);
            return mapper.Map<CourseResponseDTO>(courseRepository.UpdateAsync(toUpdate, newCourse).Result);
				
		}
		public CourseResponseDTO Delete(Guid toDelete, string username)
		{
			throw new NotImplementedException();
		}

		public CourseResponseDTO UpdateCategory(Guid toUpdate, CategoryRequestDto newCategory, string username)
		{
			throw new NotImplementedException();
		}
	}
}
