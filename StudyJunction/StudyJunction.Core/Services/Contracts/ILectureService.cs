using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
	public interface ILectureService
	{
		LectureResponseDTO Get(Guid id);
		LectureResponseDTO Get(string title);
		ICollection<LectureResponseDTO> GetAll();
		LectureResponseDTO Create(AddLectureRequestDto newLecture);
		LectureResponseDTO Update(Guid toUpdate, LectureRequestDto newData);
		LectureResponseDTO Delete(Guid id);
	}
}
