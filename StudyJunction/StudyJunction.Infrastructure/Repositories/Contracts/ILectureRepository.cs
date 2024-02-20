using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface ILectureRepository
	{
		Task<LectureDb> GetAsync(Guid id);
		Task<LectureDb> GetAsync(string title);
		Task<ICollection<LectureDb>> GetAllAsync();
		Task<IList<LectureDb>> GetAllLecturesFormCourse(string courseTitle);
        Task<LectureDb> CreateAsync(LectureDb newLecture);
		Task<LectureDb> UpdateAsync(Guid toUpdate, LectureDb newData);
		Task<LectureDb> DeleteAsync(Guid id);
		Task<bool> IsUserOwner(string userId, Guid lectureId);

    }
}
