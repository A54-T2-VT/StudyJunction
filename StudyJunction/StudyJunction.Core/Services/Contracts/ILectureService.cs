using Microsoft.AspNetCore.Http;
using StudyJunction.Core.RequestDTOs.Lecture;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
    public interface ILectureService
	{
		Task<LectureResponseDTO> Get(Guid id);
        Task<LectureResponseDTO> Get(string title);
		Task<ICollection<LectureResponseDTO>> GetAll();
		Task<LectureResponseDTO> Create(AddLectureRequestDto newLecture, string username);
		Task<LectureResponseDTO> AddAssignmentAsync(string lectureId, IFormFile assignment, string userId);
        Task<LectureResponseDTO> Update(Guid toUpdate, LectureRequestDto newData, string username);
		Task<LectureResponseDTO> Delete(Guid id, string username);
	}
}
