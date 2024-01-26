using StudyJunction.Core.RequestDTOs;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services.Contracts;

namespace StudyJunction.Core.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryResponseDTO Create(AddCategoryRequestDto newCategory)
        {
            throw new NotImplementedException();
        }

        public CategoryResponseDTO Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryResponseDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CategoryResponseDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public CategoryResponseDTO GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public CategoryResponseDTO Update(Guid id, CategoryRequestDto updatedCategory)
        {
            throw new NotImplementedException();
        }
    }
}
