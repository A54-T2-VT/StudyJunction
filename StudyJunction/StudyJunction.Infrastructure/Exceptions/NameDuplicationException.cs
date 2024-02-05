

namespace StudyJunction.Infrastructure.Exceptions
{
	public class NameDuplicationException : ApplicationException
    {
		public NameDuplicationException(string? message) : base(message)
		{
		}
	}
}
