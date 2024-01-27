using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
	public interface ICourseService
	{
		CourseResponseDTO GetCourse(Guid courseId);
		CourseResponseDTO GetCourse(string title);
		ICollection<CourseResponseDTO> GetAll();
		CourseResponseDTO Create(AddCourseRequestDto newCourse, string username);
		CourseResponseDTO Update(Guid toUpdate, CourseRequestDto newData, string username);
		CourseResponseDTO Delete(Guid toDelete, string username);
		CourseResponseDTO UpdateCategory(Guid toUpdate, CategoryRequestDto newCategory, string username);

	}
}
