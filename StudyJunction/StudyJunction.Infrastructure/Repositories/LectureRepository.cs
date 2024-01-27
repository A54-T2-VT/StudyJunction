﻿using Microsoft.EntityFrameworkCore;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using System.Xml.Linq;

namespace StudyJunction.Infrastructure.Repositories
{
	public class LectureRepository : ILectureRepository
	{
		private readonly SJDbContext context;
		public LectureRepository(SJDbContext _context)
		{
			context = _context;
		}

		public async Task<LectureDb> CreateAsync(LectureDb newLecture)
		{
			await context.Lectures.AddAsync(newLecture);
			await context.SaveChangesAsync();
			return newLecture;
		}

		public async Task<LectureDb> DeleteAsync(Guid id)
		{
			var toDelete = await context.Lectures.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.LECTURE_WITH_ID_NOT_FOUND_MESSAGE, id));

			context.Lectures.Remove(toDelete);
			context.SaveChanges();
			return toDelete;
		}

		public async Task<LectureDb> GetAsync(Guid id)
		{
			var lec = await context.Lectures.FirstOrDefaultAsync(c => c.Id.Equals(id))
			   ?? throw new EntityNotFoundException
			   (String.Format(ExceptionMessages.LECTURE_WITH_ID_NOT_FOUND_MESSAGE, id));

			return lec;
		}

		public async Task<LectureDb> GetAsync(string title)
		{
			var lec = await context.Lectures.FirstOrDefaultAsync(lec => lec.Title.Equals(title))
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.LECTURE_WITH_TITLE_NOT_FOUND_MESSAGE, title));

			return lec;
		}

		public async Task<ICollection<LectureDb>> GetAllAsync()
		{
			var lectures = await context.Lectures.ToListAsync();

			return lectures;
		}

		public async Task<LectureDb> UpdateAsync(Guid toUpdate, LectureDb newData)
		{
			var lectureToUpdate = await context.Lectures.FirstOrDefaultAsync(lec => lec.Id.Equals(toUpdate))
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.LECTURE_WITH_ID_NOT_FOUND_MESSAGE, toUpdate));

			lectureToUpdate.Title = newData.Title;
			lectureToUpdate.Description = newData.Description;

			await context.SaveChangesAsync();
			return lectureToUpdate;
		}
	}
}
