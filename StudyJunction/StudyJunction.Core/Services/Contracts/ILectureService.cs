using StudyJunction.Core.RequestDTOs.Lecture;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
    public interface ILectureService
	{
		LectureResponseDTO Get(Guid id);
		LectureResponseDTO Get(string title);
		ICollection<LectureResponseDTO> GetAll();
		LectureResponseDTO Create(AddLectureRequestDto newLecture, string username);
		LectureResponseDTO Update(Guid toUpdate, LectureRequestDto newData, string username);
		LectureResponseDTO Delete(Guid id, string username);
	}
}
