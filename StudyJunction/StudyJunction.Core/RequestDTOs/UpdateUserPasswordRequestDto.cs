

namespace StudyJunction.Core.RequestDTOs
{
    public class UpdateUserPasswordRequestDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
