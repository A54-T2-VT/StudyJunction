using AutoMapper;
using Moq;
using StudyJunction.Core.RequestDTOs.Category;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Exceptions;
using StudyJunction.Infrastructure.Repositories.Contracts;
using StudyJunction.Core.Services;

namespace StudyJunction.Test.Category
{
    [TestClass]
    public class Create_Should
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
        public async Task Returns_CategoryResponseDTO()
        {
            // Arrange
            var newCategory = new AddCategoryRequestDto { Name = "NewCategory" };

            // Configure CategoryRepository mock
            categoryRepositoryMock.Setup(repo => repo.CategoryNameExists(newCategory.Name))
                .ReturnsAsync(false); // Simulating that the category does not exist

            categoryRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<CategoryDb>())).ReturnsAsync(new CategoryDb() { Name = newCategory.Name });

            // Configure Mapper mock
            mapperMock.Setup(mapper => mapper.Map<CategoryDb>(newCategory))
                .Returns(new CategoryDb() { Name = newCategory.Name}); // Replace CategoryDb with the actual type returned by your mapper

            mapperMock.Setup(mapper => mapper.Map<CategoryResponseDTO>(It.IsAny<CategoryDb>())).Returns(new CategoryResponseDTO());

            CategoryService categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await categoryService.Create(newCategory);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CategoryResponseDTO));
            // Add additional assertions based on the expected behavior of your method
        }

        [TestMethod]
        public async Task Throws_NameDuplicationException()
        {
            // Arrange
            var newCategory = new AddCategoryRequestDto { Name = "ExistingCategory" };

            // Configure CategoryRepository mock
            categoryRepositoryMock.Setup(repo => repo.CategoryNameExists(newCategory.Name))
                .ReturnsAsync(true); // Simulating that the category already exists

            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            // Act and Assert
            var exception = await Assert.ThrowsExceptionAsync<NameDuplicationException>(() =>
            {
                return categoryService.Create(newCategory);
            });

            Assert.AreEqual(
                string.Format(ExceptionMessages.NAME_DUPLICATION_MESSAGE, newCategory.Name),
                exception.Message);
        }
    }
}
