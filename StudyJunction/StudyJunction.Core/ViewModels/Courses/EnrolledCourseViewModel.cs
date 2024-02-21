using Microsoft.AspNetCore.Http;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.ViewModels.Courses
{
    public class EnrolledCourseViewModel
    {
        public CourseResponseDTO Course { get; set; }
        public string Username { get; set; }
    }
}
