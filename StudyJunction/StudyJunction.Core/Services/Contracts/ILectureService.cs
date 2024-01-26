using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.Services.Contracts
{
	public interface ILectureService
	{
		LectureResponseDTO Get(Guid id);
		LectureResponseDTO Get(string title);
		ICollection<LectureResponseDTO> GetAll();
		LectureResponseDTO Create(AddLectureRequestDto newLecture);
		LectureResponseDTO Update(Guid toUpdate, LectureRequestDto newData);
		LectureResponseDTO Delete(Guid id);
	}
}
