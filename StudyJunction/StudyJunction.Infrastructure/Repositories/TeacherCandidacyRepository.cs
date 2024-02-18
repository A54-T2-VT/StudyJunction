using Microsoft.EntityFrameworkCore;
using StudyJunction.Infrastructure.Data;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Infrastructure.Repositories
{
	public class TeacherCandidacyRepository : ITeacherCandidacyRepository
	{
		private readonly SJDbContext context;

		public TeacherCandidacyRepository(SJDbContext context)
        {
			this.context = context;
		}
        public async Task Approve(Guid id)
		{
			var targetCandidacy = await context.TeachersCandidacies.Include(tc => tc.User).FirstOrDefaultAsync(tc => tc.Id == id) ?? throw new EntityNotFoundException($"Candidacy with id: {id.ToString()}");

			targetCandidacy.Approved = true;

			var userEmail = targetCandidacy.User.Email;

			await context.SaveChangesAsync();

			var allCandidaciesFromUser = await context.TeachersCandidacies.Include(tc => tc.User).Where(tc => tc.User.Email == userEmail && tc.Approved == false).ToListAsync();

			if (allCandidaciesFromUser.Count == 0)
			{
				return;
			}

			foreach(var candidacy in allCandidaciesFromUser)
			{
				candidacy.Approved = true;
			}

			context.TeachersCandidacies.UpdateRange(allCandidaciesFromUser);
			await context.SaveChangesAsync();
		}

		public async Task Create(TeacherCandidacyDb candidacy)
		{
			await context.TeachersCandidacies.AddAsync(candidacy);
			await context.SaveChangesAsync();

		}

		public async Task<List<TeacherCandidacyDb>> GetAll()
		{
			var all = await context.TeachersCandidacies
				.Include(tc => tc.User)
				.Where(tc => tc.Approved == false)
				.ToListAsync();

			return all;
		}
	}
}
