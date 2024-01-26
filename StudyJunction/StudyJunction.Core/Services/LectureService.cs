using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.Services
{
	public class LectureService : ILectureService
	{
		public LectureResponseDTO Create(AddLectureRequestDto newLecture)
		{
			throw new NotImplementedException();
		}

		public LectureResponseDTO Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public LectureResponseDTO Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public LectureResponseDTO Get(string title)
		{
			throw new NotImplementedException();
		}

		public ICollection<LectureResponseDTO> GetAll()
		{
			throw new NotImplementedException();
		}

		public LectureResponseDTO Update(Guid toUpdate, LectureRequestDto newData)
		{
			throw new NotImplementedException();
		}
	}
}
