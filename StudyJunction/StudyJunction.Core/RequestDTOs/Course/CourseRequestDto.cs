
using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.RequestDTOs.Course
{
    public class CourseRequestDto
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
