﻿using StudyJunction.Core.RequestDTOs;
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
        public NoteResponseDTO Create(AddNoteRequestDto newNote, string username)
        {
            throw new NotImplementedException();
        }

        public NoteResponseDTO Delete(Guid id, string username)
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

        public NoteResponseDTO Update(Guid id, NoteRequestDto updatedNote, string username)
        {
            throw new NotImplementedException();
        }
    }
}
