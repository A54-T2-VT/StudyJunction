﻿using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface IUserRepository
	{
		Task<UserDb> GetByIdAsync(string id);
		Task<UserDb> GetByEmailAsync(string email);
		Task<ICollection<UserDb>> GetAllAsync();
		Task<UserDb> CreateAsync(UserDb user);
		Task<UserDb> UpdateAsync(string toUpdateid, UserDb newData);
		Task<UserDb> DeleteAsync(string id);
		bool HasCreatedCourse(UserDb user, string courseTitle);
	}
}
