﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Infrastructure.Exceptions
{
	public class UnauthorizedUserException : Exception
	{
		public UnauthorizedUserException(string? message) : base(message)
		{
		}
	}
}