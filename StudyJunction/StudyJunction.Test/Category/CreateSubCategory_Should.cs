using AutoMapper;
using Moq;
using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.Services;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Repositories.Contracts;

namespace StudyJunction.Test.Category
{
    [TestClass]
    public class CreateSubCategory_Should
    {
        private Mock<ICategoryRepository> categoryRepositoryMock;
        private Mock<IMapper> mapperMock;

        [TestInitialize]
        public void Initialize()
        {
            mapperMock = new Mock<IMapper>();
            categoryRepositoryMock = new Mock<ICategoryRepository>();

        }

        [TestMethod]
        public async Task CreateSubCategory_When_CategoryDoesNotExist()
        {
            // Arrange
            var newCategory = new AddCategoryRequestDto { Name = "NewCategory" };
            var parentId = Guid.NewGuid();


            // Configure CategoryRepository mock
            categoryRepositoryMock.Setup(repo => repo.CategoryNameExists(newCategory.Name))
                .ReturnsAsync(false);

            categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(parentId))
                .ReturnsAsync(new CategoryDb());

            categoryRepositoryMock.Setup(repo => repo.AddSubCategory(It.IsAny<CategoryDb>(), It.IsAny<CategoryDb>()))
                .ReturnsAsync(new CategoryDb());

            // Configure Mapper mock
            mapperMock.Setup(mapper => mapper.Map<CategoryDb>(newCategory))
                .Returns(new CategoryDb()); // Replace CategoryDb with the actual type returned by your mapper

            // Configure GetByIdAsync mock
            categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(parentId))
                .ReturnsAsync(new CategoryDb()); // Replace CategoryDb with the actual type returned by your repository

            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await categoryService.CreateSubCategory(newCategory, parentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CategoryResponseDTO));
            // Add additional assertions based on the expected behavior of your method
        }

        //[TestMethod]
        //public async Task CreateSubCategory_CategoryExists_ThrowsNameDuplicationException()
        //{
        //    // Arrange
        //    var newCategory = new AddCategoryRequestDto { Name = "ExistingCategory" };
        //    var parentId = Guid.NewGuid();


        //    // Configure CategoryRepository mock
        //    categoryRepositoryMock.Setup(repo => repo.CategoryNameExists(newCategory.Name))
        //        .ReturnsAsync(true); // Simulating that the category already exists

        //    var categoryService = new YourCategoryService(categoryRepositoryMock.Object, mapperMock.Object);

        //    // Act and Assert
        //    var exception = await Assert.ThrowsAsync<NameDuplicationException>(() =>
        //    {
        //        return categoryService.CreateSubCategory(newCategory, parentId);
        //    });

        //    Assert.AreEqual(
        //        String.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCategory.Name),
        //        exception.Message);
        //}
    }
}
