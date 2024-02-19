﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyJunction.Core.Services.Contracts;
using StudyJunction.Infrastructure.Constants;

namespace StudyJunction.Web.Areas.God.Controllers
{
    [Area(RolesConstants.God)]
    [Authorize(Roles = RolesConstants.God)]
    public class CoursesController : Controller
    {
		private readonly ICourseService courseService;

		public CoursesController(ICourseService courseService)
        {
			this.courseService = courseService;
		}

        [HttpGet]
        public async Task<IActionResult> GetAllForApproval()
        {
            var coursesForApproval = await courseService.GetAllNotApproved();

            return View("ForApproval", coursesForApproval);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string courseId) 
        {
            try
            {
                await courseService.ApproveCourseAsync(new Guid(courseId));

                return RedirectToAction("GetAllForApproval", "Courses", new {area = RolesConstants.God});
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { area = RolesConstants.God });
			}
        }

    }
}
