using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.Services
{
	public class CourseService : ICourseService
	{
		public CourseResponseDTO Create(AddCourseRequestDto newCourse)
		{
			throw new NotImplementedException();
		}

		public CourseResponseDTO Delete(Guid toDelete)
		{
			throw new NotImplementedException();
		}

		public ICollection<CourseResponseDTO> GetAll()
		{
			throw new NotImplementedException();
		}

		public CourseResponseDTO GetCourse(Guid courseId)
		{
			throw new NotImplementedException();
		}

		public CourseResponseDTO GetCourse(string title)
		{
			throw new NotImplementedException();
		}

		public CourseResponseDTO Update(Guid toUpdate, CourseRequestDto newData)
		{
			throw new NotImplementedException();
		}
	}
}
