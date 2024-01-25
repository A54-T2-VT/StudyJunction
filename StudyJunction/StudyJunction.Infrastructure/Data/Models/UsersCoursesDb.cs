
namespace StudyJunction.Infrastructure.Data.Models
{
    public class UsersCoursesDb
    {
        public Guid CourseId { get; set; }
        public CourseDb Course { get; set; }
        public string UserId { get; set; }
        public UserDb User { get; set; }

        public bool IsFinished { get; set; }
    }
}
