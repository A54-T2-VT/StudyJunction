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
			var courses = await context.Courses.ToListAsync();

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
			var c = await context.Courses.FirstOrDefaultAsync(c => c.Title.Equals(title))
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.COURSE_WITH_TITLE_NOT_FOUND_MESSAGE, title));

			return c;
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
		public bool CourseTitleExists(string title)
		{
			return context.Courses.Any(x => x.Title.Equals(title));
		}
    }
}
