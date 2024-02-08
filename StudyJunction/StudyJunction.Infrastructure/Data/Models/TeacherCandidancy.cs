
namespace StudyJunction.Infrastructure.Data.Models
{
    public class TeacherCandidancy
    {
        public Guid Id { get; set; }
        public string CandidateId { get; set; }
        public UserDb User { get; set; }
        public string Credentials { get; set; }
        public bool Approved { get; set; }
    }
}
