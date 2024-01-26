using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
	public interface INoteService
	{
        IEnumerable<NoteResponseDTO> GetAll();
        NoteResponseDTO GetById(Guid id);
        NoteResponseDTO Create(AddNoteRequestDto newNote, string username);
        NoteResponseDTO Update(Guid id, NoteRequestDto updatedNote, string username);
        NoteResponseDTO Delete(Guid id, string username);
    }
}
