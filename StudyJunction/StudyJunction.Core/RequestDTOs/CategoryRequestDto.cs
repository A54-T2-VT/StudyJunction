

using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.RequestDTOs
{
    public class CategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
