using AutoMapper;
using Moq;
using StockTrackingApp.Business.Dto.Category;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Business;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace StockTrackingApp.Business.Tests
{
    public class CategoryServiceTests
    {
        private readonly ICategoryService _categoryService;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();

            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsListOfCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Furniture" }
            };

            _categoryRepositoryMock.Setup(mock => mock.GetAll()).Returns(categories);
            _mapperMock.Setup(mock => mock.Map<List<GetAllCategoriesDto>>(categories)).Returns(
                new List<GetAllCategoriesDto>
                {
                    new GetAllCategoriesDto { Id = 1, Name = "Electronics" },
                    new GetAllCategoriesDto { Id = 2, Name = "Clothing" },
                    new GetAllCategoriesDto { Id = 3, Name = "Furniture" }
                });

            // Act
            var result = _categoryService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(categories.Count, result.Result.Count);
        }

        [Fact]
        public void GetById_ExistingCategoryId_ReturnsCategory()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Electronics" };

            _categoryRepositoryMock.Setup(mock => mock.GetById(categoryId, true)).Returns(category);
            _mapperMock.Setup(mock => mock.Map<GetCategoryByIdWithProductsDto>(category)).Returns(
                new GetCategoryByIdWithProductsDto { Id = categoryId, Name = "Electronics" });

            // Act
            var result = _categoryService.GetById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(categoryId, result.Result.Id);
            Assert.Equal("Electronics", result.Result.Name);
        }

        [Fact]
        public void GetById_NonExistingCategoryId_ReturnsNotFound()
        {
            // Arrange
            var categoryId = 1;
            Category category = null;

            _categoryRepositoryMock.Setup(mock => mock.GetById(categoryId, true)).Returns(category);

            // Act
            var result = _categoryService.GetById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Null(result.Result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
        }

        [Fact]
        public void Add_ValidCategory_ReturnsAddedCategory()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto { Name = "Electronics" };
            var addedCategory = new Category { Id = 1, Name = "Electronics" };

            _categoryRepositoryMock.Setup(mock => mock.GetByName(createCategoryDto.Name,false)).Returns((Category)null);
            _categoryRepositoryMock.Setup(mock => mock.Add(It.IsAny<Category>())).Returns(addedCategory);
            _mapperMock.Setup(mock => mock.Map<GetCategoryByIdDto>(addedCategory)).Returns(
                new GetCategoryByIdDto { Id = addedCategory.Id, Name = addedCategory.Name });

            // Act
            var result = _categoryService.Add(createCategoryDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(addedCategory.Id, result.Result.Id);
            Assert.Equal("Electronics", result.Result.Name);
        }

        [Fact]
        public void Add_DuplicateCategory_ReturnsBadRequest()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto { Name = "Electronics" };
            var existingCategory = new Category { Id = 1, Name = "Electronics" };

            _categoryRepositoryMock.Setup(mock => mock.GetByName(createCategoryDto.Name,false)).Returns(existingCategory);

            // Act
            var result = _categoryService.Add(createCategoryDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Null(result.Result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.ErrorCode);
        }

        [Fact]
        public void Delete_ExistingCategoryId_ReturnsDeletedCategory()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Electronics" };

            _categoryRepositoryMock.Setup(mock => mock.GetById(categoryId,false)).Returns(category);
            _categoryRepositoryMock.Setup(mock => mock.Delete(categoryId)).Returns(category);
            _mapperMock.Setup(mock => mock.Map<GetCategoryByIdDto>(category)).Returns(
                new GetCategoryByIdDto { Id = categoryId, Name = "Electronics" });

            // Act
            var result = _categoryService.Delete(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(categoryId, result.Result.Id);
            Assert.Equal("Electronics", result.Result.Name);
        }

        [Fact]
        public void Delete_NonExistingCategoryId_ReturnsNotFound()
        {
            // Arrange
            var categoryId = 1;
            Category category = null;

            _categoryRepositoryMock.Setup(mock => mock.GetById(categoryId, false)).Returns(category);

            // Act
            var result = _categoryService.Delete(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Null(result.Result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
        }

        [Fact]
        public void Update_ExistingCategory_ReturnsUpdatedCategory()
        {
            // Arrange
            var updateCategoryDto = new UpdateCategoryDto { Id = 1, Name = "Electronics" };
            var category = new Category { Id = updateCategoryDto.Id, Name = "Electronics" };

            _categoryRepositoryMock.Setup(mock => mock.GetById(updateCategoryDto.Id, false)).Returns(category);
            _categoryRepositoryMock.Setup(mock => mock.Update(updateCategoryDto.Id, It.IsAny<Category>())).Returns(category);
            _mapperMock.Setup(mock => mock.Map<GetCategoryByIdDto>(category)).Returns(
                new GetCategoryByIdDto { Id = updateCategoryDto.Id, Name = updateCategoryDto.Name });

            // Act
            var result = _categoryService.Update(updateCategoryDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(updateCategoryDto.Id, result.Result.Id);
            Assert.Equal(updateCategoryDto.Name, result.Result.Name);
        }

        [Fact]
        public void Update_NonExistingCategory_ReturnsNotFound()
        {
            // Arrange
            var updateCategoryDto = new UpdateCategoryDto { Id = 1, Name = "Electronics" };
            Category category = null;

            _categoryRepositoryMock.Setup(mock => mock.GetById(updateCategoryDto.Id, false)).Returns(category);

            // Act
            var result = _categoryService.Update(updateCategoryDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Null(result.Result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
        }
    }
}
