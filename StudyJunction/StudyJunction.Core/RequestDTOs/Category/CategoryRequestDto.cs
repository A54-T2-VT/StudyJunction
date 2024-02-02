

using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.RequestDTOs.Category
{
    public class CategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
