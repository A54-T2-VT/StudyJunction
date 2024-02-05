using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface ICourseRepository
	{
		Task<IEnumerable<CourseDb>> GetAllAsync();
		Task<CourseDb> GetByIdAsync(Guid id);
		Task<CourseDb> GetByTitleAsync(string title);
		Task<CourseDb> CreateAsync(CourseDb newCourse);
		Task<CourseDb> UpdateAsync(Guid id, CourseDb updatedCourse);
		Task<CourseDb> DeleteAsync(Guid id);
		bool CourseTitleExists(string title);

	}
}
