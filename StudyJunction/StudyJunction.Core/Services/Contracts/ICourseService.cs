using StudyJunction.Core.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.Services.Contracts
{
	public interface ICourseService
	{
		CourseResponseDTO GetCourse(Guid courseId);
		CourseResponseDTO GetCourse(string title);
		ICollection<CourseResponseDTO> GetAll();
		CourseResponseDTO Create(/*add requestDTO*/);
		CourseResponseDTO Update(/*same*/);
		CourseResponseDTO Delete(Guid toDelete);

	}
}
