using Microsoft.Extensions.DependencyInjection;
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface ILectureRepository
	{
		Task<LectureDb> Get(Guid id);
		Task<LectureDb> Get(string title);
		Task<ICollection<LectureDb>> GetAll();
		Task<LectureDb> Create(LectureDb newLecture);
		Task<LectureDb> Update(Guid toUpdate, LectureDb newData);
		Task<LectureDb> Delete(Guid id);
	}
}
