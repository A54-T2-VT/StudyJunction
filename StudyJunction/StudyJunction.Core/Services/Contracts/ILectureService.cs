using Microsoft.AspNetCore.Http;
using StudyJunction.Core.RequestDTOs.Lecture;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
    public interface ILectureService
	{
		LectureResponseDTO Get(Guid id);
		LectureResponseDTO Get(string title);
		ICollection<LectureResponseDTO> GetAll();
		LectureResponseDTO CreateWithAssignment(AddLectureRequestDto newLecture, IFormFile assignment, string username);
		LectureResponseDTO CreateWoutAssignement(AddLectureRequestDto newLecture, string username);
		LectureResponseDTO AddAssignment(IFormFile assignement, Guid lectureID, string username);
		LectureResponseDTO Update(Guid toUpdate, LectureRequestDto newData);
		LectureResponseDTO Delete(Guid id);
	}
}
