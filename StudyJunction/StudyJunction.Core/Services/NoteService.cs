using StudyJunction.Core.RequestDTOs.Note;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Core.Services
{
    public class NoteService : INoteService
    {
		private readonly INoteRepository noteRepository;
		private readonly IUserRepository userRepository;
        public NoteService(INoteRepository _noteRepository, IUserRepository _userRepository)
        {
			noteRepository = _noteRepository;
			userRepository = _userRepository;
		}
        public async Task<NoteResponseDTO> Create(AddNoteRequestDto newNote, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<NoteResponseDTO> Delete(Guid id, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NoteResponseDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<NoteResponseDTO> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<NoteResponseDTO> Update(Guid id, NoteRequestDto updatedNote, string username)
        {
            throw new NotImplementedException();
        }
    }
}
