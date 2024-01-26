using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Core.Services
{
    public class LectureService : ILectureService
    {
		private readonly ILectureRepository lectureRepository;
		private readonly IUserRepository userRepository;
        public LectureService(ILectureRepository _lectureRepository, IUserRepository _userRepository)
        {
            lectureRepository = _lectureRepository;
			userRepository = _userRepository;
		}
        public LectureResponseDTO Create(AddLectureRequestDto newLecture, string username)
        {
            throw new NotImplementedException();
        }

        public LectureResponseDTO Delete(Guid id, string username)
        {
            throw new NotImplementedException();
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
