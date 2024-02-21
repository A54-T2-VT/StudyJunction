using Microsoft.EntityFrameworkCore;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using System.Xml.Linq;

namespace StudyJunction.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
		private readonly SJDbContext context;
		public CourseRepository(SJDbContext _context)
		{
			context = _context;
		}
		public async Task<CourseDb> CreateAsync(CourseDb newCourse)
        {
			await context.Courses.AddAsync(newCourse);
			await context.SaveChangesAsync();
			return newCourse;
		}

        public async Task<CourseDb> DeleteAsync(Guid id)
        {
			var courseToDelete = await context.Courses.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.COURSE_WITH_ID_NOT_FOUND_MESSAGE, id));

			context.Courses.Remove(courseToDelete);
			context.SaveChanges();
			return courseToDelete;
		}

        public async Task<IEnumerable<CourseDb>> GetAllAsync()
        {
			var courses = context.Courses
				.Include(x => x.CreatedBy)
				.Include(x => x.Category)
				.Where(c => c.IsApproved)
				.ToList();

			return courses;
		}

		public async Task<IEnumerable<CourseDb>> GetAllNotApprovedCourses()
		{
			var courses = await context.Courses.Include(c => c.CreatedBy).Where(c => c.IsApproved == false).ToListAsync();

			return courses;
		}

		public async Task<IEnumerable<CourseDb>> GetCoursesCreatedByUserAsync(string userId)
		{
			var courses = await context.Courses.Include(c => c.Lectures).Include(c => c.EnrolledUsers).Where(c => c.CreatorId == userId).ToListAsync();

			return courses;
		}

        public async Task<CourseDb> GetByIdAsync(Guid id)
        {
			var c = await context.Courses.FirstOrDefaultAsync(c => c.Id.Equals(id))
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.COURSE_WITH_ID_NOT_FOUND_MESSAGE, id));

			return c;
		}

        public async Task<CourseDb> GetByTitleAsync(string title)
        {
			var c = await context.Courses.Include(c => c.CreatedBy).Include(c => c.EnrolledUsers).ThenInclude(eu => eu.User).Include(c => c.Category).FirstOrDefaultAsync(c => c.Title.Equals(title))
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.COURSE_WITH_TITLE_NOT_FOUND_MESSAGE, title));

			return c;
		}

		public async Task ApproveCourseAsync(Guid courseId)
		{
			var courseToApprove = await context.Courses.FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new EntityNotFoundException
                (String.Format(ExceptionMessages.COURSE_WITH_ID_NOT_FOUND_MESSAGE, courseId));

			courseToApprove.IsApproved = true;

			await context.SaveChangesAsync();
        }

        public async Task<CourseDb> UpdateAsync(Guid id, CourseDb updatedCourse)
        {
			var toUpdate = await context.Courses.FirstOrDefaultAsync(c => c.Id.Equals(id))
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.COURSE_WITH_ID_NOT_FOUND_MESSAGE, id));

			toUpdate.Title = updatedCourse.Title;
			toUpdate.Description = updatedCourse.Description;
			toUpdate.StartDate = updatedCourse.StartDate;

			await context.SaveChangesAsync();
			return toUpdate;
		}

        public async Task<CourseDb> ChangeCourseCategory(string categoryName, Guid courseId)
		{
			var courseToUpdate = await context.Courses				
				.FirstOrDefaultAsync(c => c.Id  == courseId)
				?? throw new EntityNotFoundException(string.Format(ExceptionMessages.COURSE_WITH_ID_NOT_FOUND_MESSAGE, courseId));

			var category = await context.Categories
				.FirstOrDefaultAsync(c => c.Name == categoryName)
				?? throw new EntityNotFoundException(string.Format(ExceptionMessages.CATEGORY_WITH_NAME_NOT_FOUND_MESSAGE, categoryName));

			courseToUpdate.Category = category;
			courseToUpdate.CategoryId = category.Id;

			await context.SaveChangesAsync();

			return courseToUpdate;
		}

        public bool CourseTitleExists(string title)
		{
			return context.Courses.Any(x => x.Title.Equals(title));
		}
        public async Task<bool> IsUserOwner(string userId, Guid courseID)
        {
			var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId) ??
				throw new EntityNotFoundException(String.Format(ExceptionMessages.USER_WITH_ID_NOT_FOUND_MESSAGE, userId));

			return context.Courses.Include(c => c.CreatedBy)
				.Where(c => c.CreatorId == userId)
				.Any(c => c.Id == courseID);
        }

        public async Task<CourseDb> AddUserToCourse(CourseDb course, string username)
        {
			var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username)
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.USER_WITH_USERNAME_NOT_FOUND_MESSAGE, username));

			UsersCoursesDb usersCoursesDb = new UsersCoursesDb()
			{
				CourseId = course.Id,
				UserId = user.Id
			};
			await context.UsersCourses.AddAsync(usersCoursesDb);
			await context.SaveChangesAsync();
			return course;
        }

        public async Task<IEnumerable<CourseDb>> FilterBy(string searchValue)
        {
			var result = context.Courses.Include(x => x.CreatedBy)
				.Include(x => x.Category)
				.Where(c => c.Title.Contains(searchValue.ToLower()) && c.IsApproved)
				.ToList();

			return result;
        }

        public async Task<IEnumerable<CourseDb>> FilterByCategory(string categoryName)
        {
			var result = context.Courses
				.Include(c => c.Category)
				.Include(c => c.CreatedBy)
				.Where(c => c.Category.Name == categoryName && c.IsApproved)
				.ToList();

			return result;
        }
    }
}
