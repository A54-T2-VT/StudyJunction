using StudyJunction.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
