using CloudinaryDotNet;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.ViewModels.Courses
{
    public class CourseViewModel
    {
        public CloudinaryService Service { get; set; }
        public IEnumerable<CourseResponseDTO> Courses { get; set; }
        public IEnumerable<UserResponseDTO> Users { get; set; }
        public Dictionary<string, List<string>> ParentChildCategories { get; set; }
        public string CategoryName { get; set; }
    }
}
