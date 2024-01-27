using Microsoft.EntityFrameworkCore;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Infrastructure.Repositories
{
	public class NoteRepository : INoteRepository
	{
		private readonly SJDbContext context;
		public NoteRepository(SJDbContext _context)
		{
			context = _context;
		}

		public async Task<NoteDb> CreateAsync(NoteDb newNote)
		{
			await context.Notes.AddAsync(newNote);
			await context.SaveChangesAsync();
			return newNote;
		}

		public async Task<NoteDb> DeleteAsync(Guid id)
		{
			var toDelete = await context.Notes.FirstOrDefaultAsync(n => n.Id == id)
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.NOTE_WITH_ID_NOT_FOUND_MESSAGE, id));

			context.Notes.Remove(toDelete);
			context.SaveChanges();
			return toDelete;
		}

		public async Task<NoteDb> GetAsync(Guid id)
		{
			var note = await context.Notes.FirstOrDefaultAsync(n => n.Id.Equals(id))
			   ?? throw new EntityNotFoundException
			   (String.Format(ExceptionMessages.NOTE_WITH_ID_NOT_FOUND_MESSAGE, id));

			return note;
		}

		public async Task<ICollection<NoteDb>> GetAllAsync()
		{
			var notes = await context.Notes.ToListAsync();

			return notes;
		}

		public async Task<NoteDb> UpdateAsync(Guid toUpdate, NoteDb newData)
		{
			var noteToUpdate = await context.Notes.FirstOrDefaultAsync(n => n.Id.Equals(toUpdate))
				?? throw new EntityNotFoundException
				(String.Format(ExceptionMessages.NOTE_WITH_ID_NOT_FOUND_MESSAGE, toUpdate));

			noteToUpdate.Content = newData.Content;

			await context.SaveChangesAsync();
			return noteToUpdate;
		}
	}
}
