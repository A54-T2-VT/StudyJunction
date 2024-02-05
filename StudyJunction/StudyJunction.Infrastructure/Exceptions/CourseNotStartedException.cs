

namespace StudyJunction.Infrastructure.Exceptions
{
	public class CourseNotStartedException : ApplicationException
	{
		public CourseNotStartedException(string? message) : base(message)
		{
		}
	}
}
