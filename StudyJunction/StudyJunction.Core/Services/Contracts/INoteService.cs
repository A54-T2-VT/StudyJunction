using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
	public interface INoteService
	{
        IEnumerable<NoteResponseDTO> GetAll();
        NoteResponseDTO GetById(Guid id);
        NoteResponseDTO Create(AddNoteRequestDto newNote);
        NoteResponseDTO Update(Guid id, NoteRequestDto updatedNote);
        NoteResponseDTO Delete(Guid id);
    }
}
