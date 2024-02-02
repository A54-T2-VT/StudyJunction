using StudyJunction.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;
using static StudyJunction.Infrastructure.Constants.ModelsConstants;

namespace StudyJunction.Core.RequestDTOs.Course
{
    public class AddCourseRequestDto
    {
        [Required]
        [StringLength(CourseTitleMaxLength, MinimumLength = CourseTitleMinLength)]
        public string Title { get; set; }
        [Required]
        [StringLength(CourseDecsriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        public string CategoryName { get; set; }
        //public string CreatorName { get; set; }
    }
}
