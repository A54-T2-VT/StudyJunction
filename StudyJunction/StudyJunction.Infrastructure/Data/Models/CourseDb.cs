using StudyJunction.Infrastructure.Data.Models.Contracts;

namespace StudyJunction.Infrastructure.Data.Models
{
    public class CourseDb : ISoftDelete
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ThumbnailCloudinaryId { get; set; }
        public string? ThumbnailCloudinaryUri { get; set; }
        public string CreatorId { get; set; }
        public UserDb CreatedBy { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDb Category { get; set; }
        public DateTime? StartDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public ICollection<UsersCoursesDb> EnrolledUsers { get; set; } = new List<UsersCoursesDb>();
        public ICollection<LectureDb> Lectures { get; set; } = new List<LectureDb>();
    }
}
