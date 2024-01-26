using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
	public interface ICourseService
	{
		CourseResponseDTO GetCourse(Guid courseId);
		CourseResponseDTO GetCourse(string title);
		ICollection<CourseResponseDTO> GetAll();
		CourseResponseDTO Create(AddCourseRequestDto newCourse);
		CourseResponseDTO Update(Guid toUpdate, CourseRequestDto newData);
		CourseResponseDTO Delete(Guid toDelete);

	}
}
