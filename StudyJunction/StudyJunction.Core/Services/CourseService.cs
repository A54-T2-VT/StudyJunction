using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.RequestDTOs.Course;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories;
using StudyJunction.Infrastructure.Repositories.Contracts;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StudyJunction.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<UserDb> userManager;
        private readonly CloudinaryService cloudinaryService;
        public CourseService(ICourseRepository _courseRepository, IUserRepository _userRepository,
            IMapper _mapper, ICategoryRepository _categoryRepository, UserManager<UserDb> _userManager,
            CloudinaryService _cloudinaryService)
        {
			courseRepository = _courseRepository;
            userRepository = _userRepository;
            mapper = _mapper;
            categoryRepository = _categoryRepository;
            userManager = _userManager;
            cloudinaryService = _cloudinaryService;
		}
		
        public CourseResponseDTO Create(AddCourseRequestDto newCourse, string username)
        {
            if(courseRepository.CourseTitleExists(newCourse.Title))
            {
                throw new NameDuplicationException(
                    String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCourse.Title));
            }
            var creatorUser = userManager.FindByNameAsync(username).Result;

            var categoryDb = categoryRepository.GetByNameAsync(newCourse.CategoryName).Result;
			newCourse.CategoryName = categoryDb.Id.ToString();

            var courseDb = mapper.Map<CourseDb>(newCourse);
            courseDb.CreatorId = creatorUser.Id;
            creatorUser.MyCreatedCourses.Add(courseDb);


            return mapper.Map<CourseResponseDTO>(courseRepository.CreateAsync(courseDb).Result);
        }
        public async Task<CourseResponseDTO> AddThumbnailAsync(string courseId, IFormFile image, string userId)
        {
            if (!(await courseRepository.IsUserOwner(userId, new Guid(courseId))))
            {
                throw new UnauthorizedUserException(string.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, userId));
            }

            var courseDb = await courseRepository.GetByIdAsync(new Guid(courseId));


            var assignmentCloudinaryData = cloudinaryService.UploadImageToCloudinary(image);

            courseDb.ThumbnailURL = assignmentCloudinaryData[1];

            var result = courseRepository.UpdateAsync(courseDb.Id, courseDb);

            return mapper.Map<CourseResponseDTO>(await result);
        }
        public ICollection<CourseResponseDTO> GetAll()
        {
            return courseRepository.GetAllAsync().Result
                .Select(x => mapper.Map<CourseResponseDTO>(x))
                .ToList();
        }

        public CourseResponseDTO GetCourse(Guid courseId)
        {
            return mapper.Map<CourseResponseDTO>
                (courseRepository.GetByIdAsync(courseId).Result);
        }

        public CourseResponseDTO GetCourse(string title)
        {
            return mapper.Map<CourseResponseDTO>(courseRepository.GetByTitleAsync(title).Result);
        }

        public CourseResponseDTO Update(Guid toUpdate, CourseRequestDto newData, string username)
        {
			var user = userManager.FindByNameAsync(username).Result;
			var courseToUpdate = courseRepository.GetByIdAsync(toUpdate).Result;
			//check if user has permission to update the course
			if (!userRepository.HasCreatedCourse(user, courseToUpdate.Title))
			{
				throw new UnauthorizedUserException(
					String.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
			}

			
            if(courseToUpdate.Title != newData.Title) //check if the course title is updated
            {
				if (courseRepository.CourseTitleExists(newData.Title)) // if it is, check if it is unique
				{
					throw new NameDuplicationException(
						String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newData.Title));
				}
			}

            var newCourse = mapper.Map<CourseDb>(newData);
            return mapper.Map<CourseResponseDTO>(courseRepository.UpdateAsync(toUpdate, newCourse).Result);
				
		}
		public CourseResponseDTO Delete(Guid toDelete, string username)
		{
			var user = userManager.FindByNameAsync(username).Result;
			
			var courseToDelete = courseRepository.GetByIdAsync(toDelete).Result;
			//check if user has permission to delete the course
			if (!userRepository.HasCreatedCourse(user, courseToDelete.Title))
			{
				throw new UnauthorizedUserException(
					String.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
			}

			return mapper.Map<CourseResponseDTO>(courseRepository.DeleteAsync(toDelete));
		}

		public CourseResponseDTO UpdateCategory(Guid toUpdate, CategoryRequestDto newCategory, string username)
		{
			throw new NotImplementedException();
		}
	}
}
