using Microsoft.AspNetCore.Http;
using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.RequestDTOs.Course;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.ViewModels.Courses;

namespace StudyJunction.Core.Services.Contracts
{
    public interface ICourseService
	{
		Task<CourseResponseDTO> GetCourse(Guid courseId);
		Task<CourseResponseDTO> GetCourse(string title);
		Task<IEnumerable<CourseResponseDTO>> GetAll();
		Task<ICollection<CourseApprovalViewModel>> GetAllNotApproved();
		Task<IEnumerable<CreatedCoursesViewModel>> GetCoursesCreatedByUserAsync(string userId);
		Task<IEnumerable<CourseResponseDTO>> FilterByTitle(string searchValue);
		Task ApproveCourseAsync(Guid courseId);
        Task<CourseResponseDTO> Create(AddCourseRequestDto newCourse, string username);
		Task<CourseResponseDTO> Update(Guid toUpdate, CourseRequestDto newData, string username);
		Task<CourseResponseDTO> Delete(Guid toDelete, string username);
		Task<CourseResponseDTO> UpdateCategory(Guid toUpdate, CategoryRequestDto newCategory, string username);
		Task<CourseResponseDTO> AddThumbnailAsync(string courseId, IFormFile image, string userId);

		Task<CourseResponseDTO> EnrollUserForCourse(string courseTitle, string username);


    }
}
