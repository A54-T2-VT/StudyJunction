using StudyJunction.Infrastructure.Data.Models.Contracts;

namespace StudyJunction.Infrastructure.Data.Models
{
    public class LectureDb : ISoftDelete
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
        public CourseDb Course { get; set; }
        public string? VideoLinkCloudinaryId { get; set; }
        public string? VideoLinkCloudinaryUri { get; set; }
        public string? AssignmentCloudinaryId { get; set; }
        public string? AssignmentCloudinaryUri { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<NoteDb> Notes { get; set; } = new List<NoteDb>();
        public ICollection<UsersLecturesDb> UsersLectures { get; set; } = new List<UsersLecturesDb>();
    }
}
