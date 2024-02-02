using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.RequestDTOs.Category
{
    public class AddCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
