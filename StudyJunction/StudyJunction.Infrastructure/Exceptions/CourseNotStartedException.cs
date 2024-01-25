using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Infrastructure.Exceptions
{
	public class CourseNotStartedException : Exception
	{
		public CourseNotStartedException(string? message) : base(message)
		{
		}
	}
}
