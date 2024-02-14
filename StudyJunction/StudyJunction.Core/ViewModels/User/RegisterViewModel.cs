using StudyJunction.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = ExceptionMessages.INVALID_EMAIL_MESSAGE)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(ModelsConstants.UserFirstNameMaxLength
            , ErrorMessage = ExceptionMessages.INVALID_USER_PROPERTY_LENGTH_MESSAGE
            , MinimumLength = ModelsConstants.UserFirstNameMinLength)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(ModelsConstants.UserLastNameMaxLength
            , ErrorMessage = ExceptionMessages.INVALID_USER_PROPERTY_LENGTH_MESSAGE
            , MinimumLength = ModelsConstants.UserLastNameMinLength)]
        public string LastName { get; set; }
    }
}
