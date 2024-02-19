using Microsoft.EntityFrameworkCore;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using System.Data;
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

		public async Task<Dictionary<string, List<string>>> GetAllWithTheirRolesAsync()
		{
            var allUsersAndRoles = await context.Users
                .Join(
                    context.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new { User = user, RoleId = userRole.RoleId }
                )
                .Join(
                    context.Roles,
                    ur => ur.RoleId,
                    role => role.Id,
                    (ur, role) => new { ur.User, RoleName = role.Name }
                )
                .ToListAsync();

			var userWithRoles = new Dictionary<string, List<string>>();//<user.email, collection<user.role>>

			foreach ( var userAndRole in allUsersAndRoles ) 
			{
				var userEmail = userAndRole.User.Email;
				var userRole = userAndRole.RoleName;

				if(!userWithRoles.ContainsKey(userEmail)) 
				{
					userWithRoles.Add(userEmail, new List<string>());
				}

				userWithRoles[userEmail].Add(userRole);

			}

			return userWithRoles;
        }

		public async Task<UserDb> GetByIdAsync(string id)
		{
			var user = await context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id))
			   ?? throw new EntityNotFoundException
			   (String.Format(ExceptionMessages.USER_WITH_ID_NOT_FOUND_MESSAGE, id));

			return user;
		}

		public async Task<UserDb> GetByUsernameIncludeCourses(string username)
		{
			var user = await context.Users.Include(u => u.MyEnrolledCourses).ThenInclude(uc => uc.Course)
				.FirstOrDefaultAsync(u => u.UserName == username)
				?? throw new EntityNotFoundException
                (String.Format(ExceptionMessages.USER_WITH_USERNAME_NOT_FOUND_MESSAGE, username));

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
		public bool HasCreatedCourse(UserDb user, string courseTitle)
		{
			var userDb = context.Users.Include(x => x.MyCreatedCourses).FirstOrDefault(x => x.Id == user.Id);
			return userDb.MyCreatedCourses.Any(x => x.Title == courseTitle);
		}
	}
}
