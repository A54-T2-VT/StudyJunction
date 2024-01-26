using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public async Task<CourseDb> CreateAsync(CourseDb newCourse)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseDb> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CourseDb>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CourseDb> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseDb> GetByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseDb> UpdateAsync(Guid id, CourseDb updatedCourse)
        {
            throw new NotImplementedException();
        }
    }
}
