using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface INoteRepository
	{
		Task<NoteDb> GetAsync(Guid id);
		Task<ICollection<NoteDb>> GetAllAsync();
		Task<NoteDb> CreateAsync(NoteDb newNote);
		Task<NoteDb> UpdateAsync(Guid toUpdate, NoteDb newData);
		Task<NoteDb> DeleteAsync(Guid id);
	}
}
