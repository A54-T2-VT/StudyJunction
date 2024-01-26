using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Infrastructure.Repositories.Contracts
{
	public interface INoteRepository
	{
		Task<NoteDb> Get(Guid id);
		Task<ICollection<NoteDb>> GetAll();
		Task<NoteDb> Create(NoteDb newNote);
		Task<NoteDb> Update(Guid toUpdate, NoteDb newData);
		Task<NoteDb> Delete(Guid id);
	}
}
