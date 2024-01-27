using Microsoft.EntityFrameworkCore;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StudyJunction.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly SJDbContext context;
		public UserRepository(SJDbContext _context)
		{
			context = _context;
		}

		public async Task<UserDb> CreateAsync(UserDb user)
		{
			await context.Users.AddAsync(user);
			await context.SaveChangesAsync();
			return user;
		}

		public async Task<UserDb> DeleteAsync(string id)
		{
			var toDelete = await context.Users.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.USER_WITH_ID_NOT_FOUND_MESSAGE, id));

			context.Users.Remove(toDelete);
			context.SaveChanges();
			return toDelete;
		}

		public async Task<ICollection<UserDb>> GetAllAsync()
		{
			var users = await context.Users.ToListAsync();

			return users;
		}

		public async Task<UserDb> GetByIdAsync(string id)
		{
			var user = await context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id))
			   ?? throw new EntityNotFoundException
			   (String.Format(ExceptionMessages.USER_WITH_ID_NOT_FOUND_MESSAGE, id));

			return user;
		}

		public async Task<UserDb> GetByEmailAsync(string email)
		{
			var user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email))
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.USER_WITH_USERNAME_NOT_FOUND_MESSAGE, email));

			return user;
		}

		public async Task<UserDb> UpdateAsync(string toUpdateid, UserDb newData)
		{
			var userToUpdate = await context.Users.FirstOrDefaultAsync(u => u.Id.Equals(toUpdateid))
			?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.USER_WITH_ID_NOT_FOUND_MESSAGE, toUpdateid));

			userToUpdate.UserName = newData.UserName;
			userToUpdate.FirstName = newData.FirstName;
			userToUpdate.LastName = newData.LastName;

			await context.SaveChangesAsync();
			return userToUpdate;
		}
	}
}
