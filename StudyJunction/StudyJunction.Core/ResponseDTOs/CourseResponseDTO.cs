﻿
namespace StudyJunction.Core.ResponseDTOs
{
	public class CourseResponseDTO
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorName { get; set; }
        public string CategoryName { get; set; }
        public DateTime? StartDate { get; set; }
        public string ThumbnailCloudinaryUri { get; set; }

        public ICollection<CourseStudentResponseDto> EnrolledStudents { get; set; }
        public ICollection<LectureResponseDTO> Lectures { get; set; }
    }
}
