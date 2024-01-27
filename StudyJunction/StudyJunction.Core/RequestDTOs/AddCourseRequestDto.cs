using StudyJunction.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.RequestDTOs
{
    public class AddCourseRequestDto
    {
        [Required]
        [StringLength(ModelsConstants.CourseTitleMaxLength, MinimumLength = ModelsConstants.CourseTitleMinLength)]
        public string Title { get; set; }
        [Required]
        [StringLength(ModelsConstants.CourseDecsriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        public string CreatorName { get; set; }
    }
}
