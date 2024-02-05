using Microsoft.AspNetCore.Identity;
using StudyJunction.Infrastructure.Data.Models.Contracts;

namespace StudyJunction.Infrastructure.Data.Models
{
    public class UserDb : IdentityUser, ISoftDelete
    {
        //first name, last name, email, passwor, profile image
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfileImageCloudinaryId { get; set; }
        public string? ProfileImageCloudinaryUri { get; set; }
        public ICollection<NoteDb> MyNotes { get; set; } = new List<NoteDb>();
        public ICollection<UsersLecturesDb> MyLectures { get; set; } = new List<UsersLecturesDb>();
        public ICollection<UsersCoursesDb> MyEnrolledCourses { get; set; } = new List<UsersCoursesDb>();
        public ICollection<CourseDb> MyCreatedCourses { get; set; } = new List<CourseDb>();
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
