using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.RequestDTOs.Lecture;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
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
        public LectureResponseDTO Create(AddLectureRequestDto newLecture, string username)
        {
            var course = courseRepository.GetByTitleAsync(newLecture.CourseName).Result;
            
			if (course.Lectures.Any(x => x.Title == newLecture.Title))
			{
				throw new NameDuplicationException(
					String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newLecture.Title));
			}

			var user = userManager.FindByNameAsync(username).Result;

			if (!userRepository.HasCreatedCourse(user, course.Title))
			{
				throw new UnauthorizedUserException(
					String.Format(ExceptionMessages.UNAUTHORIZED_USER_MESSAGE, username));
			}

            var courseDb = courseRepository.GetByTitleAsync(newLecture.CourseName).Result;

            newLecture.CourseName = courseDb.Id.ToString();


			var lec = mapper.Map<LectureDb>(newLecture);
            return mapper.Map<LectureResponseDTO>(lectureRepository.CreateAsync(lec).Result);
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

        public LectureResponseDTO Delete(Guid id, string username)
        {
            return mapper.Map<LectureResponseDTO>(lectureRepository.DeleteAsync(id));
		}

        public LectureResponseDTO Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public LectureResponseDTO Get(string title)
        {
            throw new NotImplementedException();
        }

        public ICollection<LectureResponseDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public LectureResponseDTO Update(Guid toUpdate, LectureRequestDto newData, string username)
        {
            throw new NotImplementedException();
        }
    }
}
