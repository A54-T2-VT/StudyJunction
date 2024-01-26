﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Infrastructure.Constants
{
	public static class ExceptionMessages
	{
		public const string USER_WITH_ID_NOT_FOUND_MESSAGE = "User with id [{0}] not found.";
		public const string USER_WITH_USERNAME_NOT_FOUND_MESSAGE = "User with username [{0}] not found.";

		public const string COURSE_WITH_TITLE_NOT_FOUND_MESSAGE = "Course with title [{0}] not found.";
		public const string COURSE_WITH_ID_NOT_FOUND_MESSAGE = "Course with id [{0}] not found.";

		public const string LECTURE_WITH_ID_NOT_FOUND_MESSAGE = "Lecture with id [{0}] not found.";
		public const string LECTURE_WITH_TITLE_NOT_FOUND_MESSAGE = "Lecture with title [{0}] not found.";

		public const string CATEGORY_WITH_NAME_NOT_FOUND_MESSAGE = "Category with name [{0}] not found.";
		public const string CATEGORY_WITH_ID_NOT_FOUND_MESSAGE = "Category with id [{0}] not found.";

		public const string UNAUTHORIZED_USER_MESSAGE = "User [{0}] not authorized for this action.";

		public const string DUPLICATE_EMAIL_EXCEPTION_MESSAGE = "User with email [{0}] already exists.";

		public const string NOTE_WITH_ID_NOT_FOUND_MESSAGE = "Note with id [{0}] not found.";

		public const string NAME_DUPLICATION_MESSAGE = "Name {0} already exists.";

		public const string INVALID_CREDENTIALS_MESSAGE = "Invalid credentials.";

		public const string COURSE_NOT_STARTED_MESSAGE = "Course hasn't started yet.";
	}
}
