using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.ViewModels.Lectures
{
    public class AddLectureViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(50, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Title { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(3000, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Description { get; set; }

        public string CourseTitle { get; set; }

        public IFormFile Video { get; set; }
        public IFormFile Assignment { get; set; }

    }
}
