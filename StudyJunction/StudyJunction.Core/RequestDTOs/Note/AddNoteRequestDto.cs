using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.RequestDTOs.Note
{
    public class AddNoteRequestDto
    {
        [Required]
        public string Content { get; set; }
        public string CreatorName { get; set; }
        public string LectureName { get; set; }
    }
}
