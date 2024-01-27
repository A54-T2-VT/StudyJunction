

namespace StudyJunction.Core.ResponseDTOs
{
	public class UserResponseDTO
	{
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //might need to add more props in future
    }
}
