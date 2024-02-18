using Microsoft.AspNetCore.Identity;
using StudyJunction.Core.ExternalApis;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJunction.Core.ViewModels.Courses
{
    public class MyCoursesViewModel
    {
        public UserResponseDTO CurrentUser{ get; set; }
        public CloudinaryService Service { get; set; }
        public UserManager<UserDb> userManager { get; set; }
    }
}
