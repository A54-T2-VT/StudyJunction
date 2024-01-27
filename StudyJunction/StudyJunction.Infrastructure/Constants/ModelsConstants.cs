

namespace StudyJunction.Infrastructure.Constants
{
    public static class ModelsConstants
    {
        //User
        public const int UserFirstNameMaxLength = 20;
        public const int UserFirstNameMinLength = 2;
        public const int UserLastNameMaxLength = 20;
        public const int UserLastNameMinLength = 2;
        public const int UserPasswordMinLength = 8;

        //Course
        public const int CourseTitleMaxLength = 50;
        public const int CourseTitleMinLength = 5;
        public const int CourseDecsriptionMaxLength = 1000;

        //Lecture
        public const int LectureTitleMaxLength = 50;
        public const int LectureTitleMinLength = 5;
        public const int LectureDescriptionMaxLength = 1000;

        //Note
        public const int NoteContentMaxLength = 500;

        //Category
        public const int CategoryNameMaxLength = 20;
    }
}
