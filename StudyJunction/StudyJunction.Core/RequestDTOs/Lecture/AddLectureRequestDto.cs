using Microsoft.AspNetCore.Http;
using StudyJunction.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.RequestDTOs.Lecture
{
    public class AddLectureRequestDto
    {
        [Required]
        [StringLength(ModelsConstants.LectureTitleMaxLength, MinimumLength = ModelsConstants.LectureTitleMinLength)]
        public string Title { get; set; }
        [Required]
        [StringLength(ModelsConstants.LectureDescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        public string CourseName { get; set; }

        //public IFormFile Assignment { get; set; }
    }
}
