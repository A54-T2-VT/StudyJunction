using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IUserRepository userRepository;
        public CourseService(ICourseRepository _courseRepository, IUserRepository _userRepository)
        {
			courseRepository = _courseRepository;
            userRepository = _userRepository;
		}
        public CourseResponseDTO Create(AddCourseRequestDto newCourse, string username)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
