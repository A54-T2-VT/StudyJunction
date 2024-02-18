

namespace StudyJunction.Core.ViewModels.Categories
{
	public class AddCategoryViewModel
	{
        public string Name { get; set; }
        public string ParentCategory { get; set; }
        public Dictionary<string, List<string>> ParentChildCategories { get; set; }
    }
}
