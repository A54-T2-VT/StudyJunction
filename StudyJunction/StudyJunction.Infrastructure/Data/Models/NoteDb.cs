
using StudyJunction.Infrastructure.Data.Models.Contracts;

namespace StudyJunction.Infrastructure.Data.Models
{
    public class NoteDb : ISoftDelete
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid LectureId { get; set; }
        public LectureDb Lecture { get; set; }
        public string CreatorId { get; set; }
        public UserDb CreatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
