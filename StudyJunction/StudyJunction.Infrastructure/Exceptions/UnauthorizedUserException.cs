

namespace StudyJunction.Infrastructure.Exceptions
{
	public class UnauthorizedUserException : ApplicationException
    {
		public UnauthorizedUserException(string? message) : base(message)
		{
		}
	}
}
