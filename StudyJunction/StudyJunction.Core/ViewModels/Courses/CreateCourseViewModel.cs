using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.ViewModels.Courses
{
    public class CreateCourseViewModel
    {
        [Required]
        [MinLength(20, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(100, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Title { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(3000, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Description { get; set; }

        
        public IFormFile Thumbnail { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public DateTime StartDate { get; set; }
    }
}
