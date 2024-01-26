using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		public Task<UserDb> Create(UserDb user)
		{
			throw new NotImplementedException();
		}

		public Task<UserDb> Delete(string id)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<UserDb>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<UserDb> GetUser(string id)
		{
			throw new NotImplementedException();
		}

		public Task<UserDb> GetUserByUsername(string username)
		{
			throw new NotImplementedException();
		}

		public Task<UserDb> Update(string toUpdateid, UserDb newData)
		{
			throw new NotImplementedException();
		}
	}
}
