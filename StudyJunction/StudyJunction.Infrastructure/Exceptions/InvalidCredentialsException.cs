using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Infrastructure.Exceptions
{
	public class InvalidCredentialsException : Exception
	{
		public InvalidCredentialsException(string? message) : base(message)
		{
		}
	}
}
