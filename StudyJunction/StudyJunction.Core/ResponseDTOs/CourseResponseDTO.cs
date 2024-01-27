
namespace StudyJunction.Core.ResponseDTOs
{
	public class CourseResponseDTO
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorName { get; set; }
        public string CategoryName { get; set; }
        public DateTime? StartDate { get; set; }

        public ICollection<CourseStudentResponseDto> EnrolledStudents { get; set; }
        public ICollection<CourseLectureResponseDto> Lectures { get; set; }
    }
}
