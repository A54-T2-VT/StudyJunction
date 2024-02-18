using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.ViewModels.TeacherCandidacy
{
	public class AddTeacherCandidacyViewModel
	{
        [Required]
        public string Credentials { get; set; }
    }
}
