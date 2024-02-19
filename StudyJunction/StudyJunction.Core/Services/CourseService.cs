using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.RequestDTOs.Course;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Courses;
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
		
        public async Task<CourseResponseDTO> Create(AddCourseRequestDto newCourse, string username)
        {
            if(courseRepository.CourseTitleExists(newCourse.Title))
            {
                throw new NameDuplicationException(
                    String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCourse.Title));
            }
            var creatorUser = await userManager.FindByNameAsync(username);

            var categoryDb = await categoryRepository.GetByNameAsync(newCourse.CategoryName);
			newCourse.CategoryName = categoryDb.Name;

            var thumbnailData = cloudinaryService.UploadImageToCloudinary(newCourse.Thumbnail);

            var courseDb = mapper.Map<CourseDb>(newCourse);
            courseDb.CategoryId = categoryDb.Id;
            courseDb.CreatorId = creatorUser.Id;
            courseDb.ThumbnailCloudinaryId = thumbnailData[0];
            courseDb.ThumbnailCloudinaryUri = thumbnailData[1];
            creatorUser.MyCreatedCourses.Add(courseDb);


            return mapper.Map<CourseResponseDTO>(await courseRepository.CreateAsync(courseDb));
        }
        public async Task<CourseResponseDTO> AddThumbnailAsync(string courseId, IFormFile image, string userId)
        {
            if (!(await courseRepository.IsUserOwner(userId, new Guid(courseId))))
            {
                throw new UnauthorizedUserException(string.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, userId));
            }

            var courseDb = await courseRepository.GetByIdAsync(new Guid(courseId));


            var thumbnailCloudinaryData = cloudinaryService.UploadImageToCloudinary(image);

            courseDb.ThumbnailCloudinaryUri = thumbnailCloudinaryData[1];

            var result = courseRepository.UpdateAsync(courseDb.Id, courseDb);

            return mapper.Map<CourseResponseDTO>(await result);
        }
        public async Task<ICollection<CourseResponseDTO>> GetAll()
        {
            var courses = await courseRepository.GetAllAsync();

            return courses
                .Select(x => mapper.Map<CourseResponseDTO>(x))
                .ToList();
        }

        public async Task<ICollection<CourseApprovalViewModel>> GetAllNotApproved()
        {
            var courses = await courseRepository.GetAllNotApprovedCourses();

            return courses
                .Select(x => mapper.Map<CourseApprovalViewModel>(x))
                .ToList();
        }

		public async Task<IEnumerable<CreatedCoursesViewModel>> GetCoursesCreatedByUserAsync(string userId)
        {
            var coursesDb = await courseRepository.GetCoursesCreatedByUserAsync(userId);

            var models = new List<CreatedCoursesViewModel>(); 

            foreach(CourseDb courseDb in coursesDb)
            {
                var model = new CreatedCoursesViewModel();

                model.Title = courseDb.Title;
                model.IsApproved = courseDb.IsApproved;
                model.NumberOfLectures = courseDb.Lectures.Count;
                model.NumberOfEnrolledStudents = courseDb.EnrolledUsers.Count;

                models.Add(model);
            }

            return models;
        }


		public async Task<CourseResponseDTO> GetCourse(Guid courseId)
        {
            return mapper.Map<CourseResponseDTO>
                (await courseRepository.GetByIdAsync(courseId));
        }

        public async Task<CourseResponseDTO> GetCourse(string title)
        {
            return mapper.Map<CourseResponseDTO>(await courseRepository.GetByTitleAsync(title));
        }

        public async Task ApproveCourseAsync(Guid courseId)
        {
            await courseRepository.ApproveCourseAsync(courseId);
        }

        public async Task<CourseResponseDTO> Update(Guid toUpdate, CourseRequestDto newData, string username)
        {
			var user = await userManager.FindByNameAsync(username);
			var courseToUpdate = await courseRepository.GetByIdAsync(toUpdate);
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
            return mapper.Map<CourseResponseDTO>( await courseRepository.UpdateAsync(toUpdate, newCourse));				
		}
		public async Task<CourseResponseDTO> Delete(Guid toDelete, string username)
		{
			var user = await userManager.FindByNameAsync(username);
			
			var courseToDelete = await courseRepository.GetByIdAsync(toDelete);
			//check if user has permission to delete the course
			if (!userRepository.HasCreatedCourse(user, courseToDelete.Title))
			{
				throw new UnauthorizedUserException(
					String.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
			}

			return mapper.Map<CourseResponseDTO>(await courseRepository.DeleteAsync(toDelete));
		}

		public async Task<CourseResponseDTO> UpdateCategory(Guid toUpdate, CategoryRequestDto newCategory, string userId)
		{
            if(await courseRepository.IsUserOwner(userId, toUpdate))
            {
                throw new UnauthorizedUserException(string.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, userId ));
            }

            if(!(await categoryRepository.CategoryNameExists(newCategory.Name)))
            {
                throw new EntityNotFoundException(string.Format(ExceptionMessages.CATEGORY_WITH_NAME_NOT_FOUND_MESSAGE, newCategory.Name));
            }

            var updatedCourseDb = await courseRepository.ChangeCourseCategory(newCategory.Name, toUpdate);

            return mapper.Map<CourseResponseDTO>(updatedCourseDb);

		}

        public async Task<CourseResponseDTO> EnrollUserForCourse(string username, string courseTitle)
        {
            var course = await courseRepository.GetByTitleAsync(courseTitle);

            var result = await courseRepository.AddUserToCourse(course, username);

            return mapper.Map<CourseResponseDTO>(result);
        }
    }
}
