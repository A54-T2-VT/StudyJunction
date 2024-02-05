using Microsoft.AspNetCore.Http;

namespace StudyJunction.Core.ResponseDTOs
{
	public class LectureResponseDTO
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public ICollection<LectureNoteResponseDto> StudentNotes { get; set; }
    }
}
