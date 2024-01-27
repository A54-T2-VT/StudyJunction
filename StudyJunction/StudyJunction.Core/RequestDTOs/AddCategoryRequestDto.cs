using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.RequestDTOs
{
    public class AddCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
