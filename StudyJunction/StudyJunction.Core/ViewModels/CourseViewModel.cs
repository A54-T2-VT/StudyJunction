using CloudinaryDotNet;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.ViewModels
{
    public class CourseViewModel
    {
        public CloudinaryService Service { get; set; }
        public ICollection<CourseResponseDTO> Courses { get; set; }
    }
}
