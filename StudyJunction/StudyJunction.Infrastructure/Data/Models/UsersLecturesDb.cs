

namespace StudyJunction.Infrastructure.Data.Models
{
    public class UsersLecturesDb
    {
        public Guid LectureId { get; set; }
        public LectureDb Lecture { get; set; }
        public string UserId { get; set; }
        public UserDb User { get; set; }

        public string? AssignmentCloudinaryId { get; set; }
        public string? AssignmentCloudinaryUri { get; set; }

        public bool IsFinished { get; set; }
    }
}
