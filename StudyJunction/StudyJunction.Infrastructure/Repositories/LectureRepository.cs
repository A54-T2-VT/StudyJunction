using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Infrastructure.Repositories
{
	public class LectureRepository : ILectureRepository
	{
		public Task<LectureDb> Create(LectureDb newLecture)
		{
			throw new NotImplementedException();
		}

		public Task<LectureDb> Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<LectureDb> Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<LectureDb> Get(string title)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<LectureDb>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<LectureDb> Update(Guid toUpdate, LectureDb newData)
		{
			throw new NotImplementedException();
		}
	}
}
