

namespace StudyJunction.Infrastructure.Data.Models
{
    public class UsersLecturesDb
    {
        public Guid LectureId { get; set; }
        public LectureDb Lecture { get; set; }
        public string UserId { get; set; }
        public UserDb User { get; set; }

        public byte[]? UserAssignment { get; set; }

        public bool IsFinished { get; set; }
    }
}
