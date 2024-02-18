

using StudyJunction.Infrastructure.Data.Models;

namespace StudyJunction.Core.ResponseDTOs
{
	public class UserResponseDTO
	{
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageCloudinaryUri { get; set; }

        public ICollection<CourseResponseDTO> MyEnrolledCourses { get; set; }

        //might need to add more props in future
    }
}
