﻿using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Infrastructure.Repositories
{
	public class NoteRepository : INoteRepository
	{
		public Task<NoteDb> Create(NoteDb newNote)
		{
			throw new NotImplementedException();
		}

		public Task<NoteDb> Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<NoteDb> Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<NoteDb>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<NoteDb> Update(Guid toUpdate, NoteDb newData)
		{
			throw new NotImplementedException();
		}
	}
}
