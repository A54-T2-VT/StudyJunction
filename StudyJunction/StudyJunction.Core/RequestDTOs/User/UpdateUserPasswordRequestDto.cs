namespace StudyJunction.Core.RequestDTOs.User
{
    public class UpdateUserPasswordRequestDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
