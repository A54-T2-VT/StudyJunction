
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface ITeacherCandidacyRepository
	{
		Task Create(TeacherCandidacyDb candidacy);
		Task Approve(Guid id);
		Task<List<TeacherCandidacyDb>> GetAll();
	}
}
