﻿using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface ICourseRepository
	{
		Task<IEnumerable<CourseDb>> GetAllAsync();
		Task<IEnumerable<CourseDb>> GetAllNotApprovedCourses();
		Task<IEnumerable<CourseDb>> GetCoursesCreatedByUserAsync(string userId);
		Task<IEnumerable<CourseDb>> FilterBy(string searchValue);
		Task<IEnumerable<CourseDb>> FilterByCategory(string categoryName);
        Task<CourseDb> GetByIdAsync(Guid id);
		Task<CourseDb> GetByTitleAsync(string title);
		Task<CourseDb> CreateAsync(CourseDb newCourse);
		Task<CourseDb> UpdateAsync(Guid id, CourseDb updatedCourse);
		Task ApproveCourseAsync(Guid courseId);
        Task<CourseDb> DeleteAsync(Guid id);
		bool CourseTitleExists(string title);
		Task<bool> IsUserOwner(string userId, Guid courseID);
		Task<CourseDb> ChangeCourseCategory(string categoryName, Guid courseId);

		Task<CourseDb> AddUserToCourse(CourseDb course, string username);

    }
}
