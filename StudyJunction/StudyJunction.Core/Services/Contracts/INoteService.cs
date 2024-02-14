using StudyJunction.Core.RequestDTOs.Note;
using StudyJunction.Core.ResponseDTOs;

namespace StudyJunction.Core.Services.Contracts
{
    public interface INoteService
	{
        Task<IEnumerable<NoteResponseDTO>> GetAll();
        Task<NoteResponseDTO> GetById(Guid id);
        Task<NoteResponseDTO> Create(AddNoteRequestDto newNote, string username);
        Task<NoteResponseDTO> Update(Guid id, NoteRequestDto updatedNote, string username);
        Task<NoteResponseDTO> Delete(Guid id, string username);
    }
}
