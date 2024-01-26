using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface IUserRepository
	{
		Task<UserDb> GetUser(string id);
		Task<UserDb> GetUserByUsername(string username);
		Task<ICollection<UserDb>> GetAll();
		Task<UserDb> Create(UserDb user);
		Task<UserDb> Update(string toUpdateid, UserDb newData);
		Task<UserDb> Delete(string id);
	}
}
