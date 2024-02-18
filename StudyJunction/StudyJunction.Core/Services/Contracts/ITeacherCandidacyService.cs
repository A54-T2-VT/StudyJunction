using StudyJunction.Core.ViewModels.TeacherCandidacy;
using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Core.Services.Contracts
{
	public interface ITeacherCandidacyService
	{
		Task Create(AddTeacherCandidacyViewModel model, UserDb candidate);
		Task Approve(Guid id);
		Task<List<TeacherCandidacyViewModel>> GetAll();
	}
}
