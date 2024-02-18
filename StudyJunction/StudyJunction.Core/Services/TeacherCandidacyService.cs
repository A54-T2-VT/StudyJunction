
using AutoMapper;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Core.ViewModels.TeacherCandidacy;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Core.Services
{
	public class TeacherCandidacyService : ITeacherCandidacyService
	{
		private readonly ITeacherCandidacyRepository teacherCandidacyRepo;
		private readonly IMapper mapper;
        private readonly IUserService userService;

        public TeacherCandidacyService(ITeacherCandidacyRepository teacherCandidacyRepo,
			IMapper mapper,
			IUserService userService)
        {
			this.teacherCandidacyRepo = teacherCandidacyRepo;
			this.mapper = mapper;
            this.userService = userService;
        }

        public async Task Approve(Guid id)
		{
			
			await teacherCandidacyRepo.Approve(id);
		}

		public async Task Create(AddTeacherCandidacyViewModel model, UserDb candidate)
		{
			var candidacy = new TeacherCandidacyDb();
			candidacy.Credentials = model.Credentials;
			candidacy.CandidateId = candidate.Id;
			
			await teacherCandidacyRepo.Create(candidacy);
		}

		public async Task<List<TeacherCandidacyViewModel>> GetAll()
		{
			var candidaciesDb = await teacherCandidacyRepo.GetAll();

			var models = candidaciesDb.Select(c => mapper.Map<TeacherCandidacyViewModel>(c)).ToList();

			return models;
		}
	}
}
