using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.RequestDTOs.Lecture;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.Lectures;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Core.Services
{
    public class LectureService : ILectureService
    {
		private readonly ILectureRepository lectureRepository;
        private readonly ICourseRepository courseRepository;
		private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
		private readonly UserManager<UserDb> userManager;
        private readonly CloudinaryService cloudinaryService;

        public LectureService(ILectureRepository _lectureRepository,
            IUserRepository _userRepository,
            IMapper _mapper,
            ICourseRepository _courseRepository,
            UserManager<UserDb> _userManager,
            CloudinaryService cloudinaryService)
        {
            lectureRepository = _lectureRepository;
			userRepository = _userRepository;
            mapper = _mapper;
            courseRepository = _courseRepository;
            userManager = _userManager;
            this.cloudinaryService = cloudinaryService;
        }
        public async Task<LectureResponseDTO> Create(AddLectureRequestDto newLecture, string username)
        {
            //TODO: Add logic for adding assignment
            var course = await courseRepository.GetByTitleAsync(newLecture.CourseName);
            
			if (course.Lectures.Any(x => x.Title == newLecture.Title))
			{
				throw new NameDuplicationException(
					String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newLecture.Title));
			}

			var user = await userManager.FindByNameAsync(username);

			if (!userRepository.HasCreatedCourse(user, course.Title))
			{
				throw new UnauthorizedUserException(
					String.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
			}

            var courseDb = await courseRepository.GetByTitleAsync(newLecture.CourseName);

            newLecture.CourseName = courseDb.Id.ToString();


			var lec = mapper.Map<LectureDb>(newLecture);
            return mapper.Map<LectureResponseDTO>(await lectureRepository.CreateAsync(lec));
		}

        public async Task<LectureResponseDTO> CreateWithVideoAndAssignmentFromViewModel(AddLectureViewModel model, string username)
        {
            var course = await courseRepository.GetByTitleAsync(model.CourseTitle);

            if (course.Lectures.Any(x => x.Title == model.Title))
            {
                throw new NameDuplicationException(
                    String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, model.Title));
            }

            var user = await userManager.FindByNameAsync(username);

            if (!userRepository.HasCreatedCourse(user, course.Title))
            {
                throw new UnauthorizedUserException(
                    String.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
            }

            var courseDb = await courseRepository.GetByTitleAsync(model.CourseTitle);

            var courseId = courseDb.Id;

            var videoData = cloudinaryService.UploadVideoToCloudinary(model.Video);
            var assignmentData = cloudinaryService.UploadPdfToCloudinary(model.Assignment);

            var lectureDb = new LectureDb()
            {
                Title = model.Title,
                Description = model.Description,
                CourseId = courseId,
                VideoLinkCloudinaryId = videoData[0],
                VideoLinkCloudinaryUri = videoData[1],
                AssignmentCloudinaryId = assignmentData[0],
                AssignmentCloudinaryUri = assignmentData[1]
            };


            return mapper.Map<LectureResponseDTO>(await lectureRepository.CreateAsync(lectureDb));
        }

        public async Task<LectureResponseDTO> AddAssignmentAsync(string lectureId, IFormFile assignment, string userId)
        {
            if(!(await lectureRepository.IsUserOwner(userId, new Guid(lectureId))))
            {
                throw new UnauthorizedUserException(string.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, userId));
            }
            
            var lectureDb = await lectureRepository.GetAsync(new Guid(lectureId));


            var assignmentCloudinaryData = cloudinaryService.UploadPdfToCloudinary(assignment);

            lectureDb.AssignmentCloudinaryId = assignmentCloudinaryData[0];
            lectureDb.AssignmentCloudinaryUri = assignmentCloudinaryData[1];

            var result = lectureRepository.UpdateAsync(lectureDb.Id, lectureDb);

            return mapper.Map<LectureResponseDTO>(await result);
        }

        public async Task<string> GetAssignmentId(string lectureTitle)
        {
            var lectureDb = await lectureRepository.GetAsync(lectureTitle);

            string assignmentId = lectureDb.AssignmentCloudinaryId;

            return assignmentId;
        }

        public async Task<LectureResponseDTO> Delete(Guid id, string username)
        {
            return mapper.Map<LectureResponseDTO>( await lectureRepository.DeleteAsync(id));
		}
		public async Task<LectureResponseDTO> Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<LectureResponseDTO> Get(string title)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<LectureResponseDTO>> GetAll()
		{
			throw new NotImplementedException();
		}
		public async Task<LectureResponseDTO> Update(Guid toUpdate, LectureRequestDto newData, string username)
		{
			throw new NotImplementedException();
		}
	}
}
