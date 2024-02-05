

namespace StudyJunction.Infrastructure.Exceptions
{
	public class InvalidCredentialsException : ApplicationException
    {
		public InvalidCredentialsException(string? message) : base(message)
		{
		}
	}
}
