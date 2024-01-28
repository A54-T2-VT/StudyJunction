using StudyJunction.Infrastructure.Data.Models.Contracts;

namespace StudyJunction.Infrastructure.Data.Models
{
    public class CategoryDb :ISoftDelete
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public CategoryDb? ParentCategory { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public ICollection<CourseDb> Courses { get; set; } = new List<CourseDb>();
    }
}
