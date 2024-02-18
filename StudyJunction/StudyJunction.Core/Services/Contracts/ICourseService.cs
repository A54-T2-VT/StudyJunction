using Microsoft.AspNetCore.Http;
using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.RequestDTOs.Course;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
    public interface ICourseService
	{
		Task<CourseResponseDTO> GetCourse(Guid courseId);
		Task<CourseResponseDTO> GetCourse(string title);
		Task<ICollection<CourseResponseDTO>> GetAll();
		Task<CourseResponseDTO> Create(AddCourseRequestDto newCourse, string username);
		Task<CourseResponseDTO> Update(Guid toUpdate, CourseRequestDto newData, string username);
		Task<CourseResponseDTO> Delete(Guid toDelete, string username);
		Task<CourseResponseDTO> UpdateCategory(Guid toUpdate, CategoryRequestDto newCategory, string username);
		Task<CourseResponseDTO> AddThumbnailAsync(string courseId, IFormFile image, string userId);

		Task<CourseResponseDTO> EnrollUserForCourse(string courseTitle, string username);


    }
}
