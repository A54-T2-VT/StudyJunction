using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;

namespace StudyJunction.Core.Services
{
    public class NoteService : INoteService
    {
        public NoteResponseDTO Create(AddNoteRequestDto newNote)
        {
            throw new NotImplementedException();
        }

        public NoteResponseDTO Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoteResponseDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public NoteResponseDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public NoteResponseDTO Update(Guid id, NoteRequestDto updatedNote)
        {
            throw new NotImplementedException();
        }
    }
}
