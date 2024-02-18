using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.ViewModels.Courses
{
    public class DetailsViewModel
    {
        public CourseResponseDTO Course { get; set; }
        public CloudinaryService Service { get; set; }

        public bool SuccessfulEnrollment { get; set; } = false;
    }
}
