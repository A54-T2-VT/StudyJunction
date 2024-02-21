

namespace StudyJunction.Core.ViewModels.Lectures
{
    public class LectureViewModel
    {
        public string Title { get; set; }
        public string CourseTitle { get; set; }
        public string Description { get; set; }
        public string VideoId { get; set; }
        public string VideoUri { get; set; }
        public string AssignmentId { get; set; }
        public string AssignmentUri { get; set; }
        public List<string> LecturesTitles { get; set; } = new List<string>();
    }
}
