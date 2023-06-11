using AutoMapper;
using Moq;
using StockTrackingApp.Business.Dto.Brand;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace StockTrackingApp.Business.Tests
{
    public class BrandServiceTests
    {
        private readonly Mock<IBrandRepository> _brandRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IBrandService _brandService;

        public BrandServiceTests()
        {
            _brandRepositoryMock = new Mock<IBrandRepository>();
            _mapperMock = new Mock<IMapper>();
            _brandService = new BrandService(_brandRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Add_ValidBrand_ReturnsAddedBrand()
        {
            // Arrange
            var createBrandDto = new CreateBrandDto { Name = "Nike" };
            var brand = new Brand { Id = 1, Name = "Nike" };

            _brandRepositoryMock.Setup(mock => mock.GetByName(createBrandDto.Name,false)).Returns((Brand)null);
            _brandRepositoryMock.Setup(mock => mock.Add(It.IsAny<Brand>())).Returns(brand);
            _mapperMock.Setup(mock => mock.Map<Brand>(createBrandDto)).Returns(brand);
            _mapperMock.Setup(mock => mock.Map<GetBrandByIdDto>(brand)).Returns(
                new GetBrandByIdDto { Id = 1, Name = "Nike" });

            // Act
            var result = _brandService.Add(createBrandDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(brand.Id, result.Result.Id);
            Assert.Equal(brand.Name, result.Result.Name);
        }

        [Fact]
        public void Add_ExistingBrand_ReturnsError()
        {
            // Arrange
            var createBrandDto = new CreateBrandDto { Name = "Nike" };
            var existingBrand = new Brand { Id = 1, Name = "Nike" };

            _brandRepositoryMock.Setup(mock => mock.GetByName(createBrandDto.Name, false)).Returns(existingBrand);

            // Act
            var result = _brandService.Add(createBrandDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Brand already exists", result.ErrorMessage);
        }

        [Fact]
        public void Delete_ExistingBrand_ReturnsDeletedBrand()
        {
            // Arrange
            var id = 1;
            var existingBrand = new Brand { Id = id, Name = "Nike" };

            _brandRepositoryMock.Setup(mock => mock.GetById(id, false)).Returns(existingBrand);
            _brandRepositoryMock.Setup(mock => mock.Delete(id)).Returns(existingBrand);
            _mapperMock.Setup(mock => mock.Map<GetBrandByIdDto>(existingBrand)).Returns(
                new GetBrandByIdDto { Id = id, Name = "Nike" });

            // Act
            var result = _brandService.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(existingBrand.Id, result.Result.Id);
            Assert.Equal(existingBrand.Name, result.Result.Name);
        }

        [Fact]
        public void Delete_NonExistingBrand_ReturnsError()
        {
            // Arrange
            var id = 1;
            _brandRepositoryMock.Setup(mock => mock.GetById(id, false)).Returns((Brand)null);

            // Act
            var result = _brandService.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Brand not found", result.ErrorMessage);
        }

        [Fact]
        public void GetAll_ReturnsAllBrands()
        {
            // Arrange
            var brands = new List<Brand>
            {
                new Brand { Id = 1, Name = "Nike" },
                new Brand { Id = 2, Name = "Adidas" },
                new Brand { Id = 3, Name = "Puma" }
            };

            _brandRepositoryMock.Setup(mock => mock.GetAll()).Returns(brands);
            _mapperMock.Setup(mock => mock.Map<List<GetAllBrandsDto>>(brands)).Returns(
                new List<GetAllBrandsDto>
                {
                    new GetAllBrandsDto { Id = 1, Name = "Nike" },
                    new GetAllBrandsDto { Id = 2, Name = "Adidas" },
                    new GetAllBrandsDto { Id = 3, Name = "Puma" }
                });

            // Act
            var result = _brandService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(3, result.Result.Count);
        }

        [Fact]
        public void GetById_ExistingBrand_ReturnsBrand()
        {
            // Arrange
            var id = 1;
            var brand = new Brand { Id = id, Name = "Nike" };

            _brandRepositoryMock.Setup(mock => mock.GetById(id, false)).Returns(brand);
            _mapperMock.Setup(mock => mock.Map<GetBrandByIdWithProductsDto>(brand)).Returns(
                new GetBrandByIdWithProductsDto { Id = id, Name = "Nike" });

            // Act
            var result = _brandService.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(brand.Id, result.Result.Id);
            Assert.Equal(brand.Name, result.Result.Name);
        }

        [Fact]
        public void GetById_NonExistingBrand_ReturnsError()
        {
            // Arrange
            var id = 1;
            _brandRepositoryMock.Setup(mock => mock.GetById(id, false)).Returns((Brand)null);

            // Act
            var result = _brandService.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Brand not found", result.ErrorMessage);
        }

        [Fact]
        public void GetByName_ExistingBrand_ReturnsBrand()
        {
            // Arrange
            var name = "Nike";
            var brand = new Brand { Id = 1, Name = name };

            _brandRepositoryMock.Setup(mock => mock.GetByName(name, false)).Returns(brand);
            _mapperMock.Setup(mock => mock.Map<GetBrandByIdWithProductsDto>(brand)).Returns(
                new GetBrandByIdWithProductsDto { Id = 1, Name = name });

            // Act
            var result = _brandService.GetByName(name);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(brand.Id, result.Result.Id);
            Assert.Equal(brand.Name, result.Result.Name);
        }

        [Fact]
        public void GetByName_NonExistingBrand_ReturnsError()
        {
            // Arrange
            var name = "Nike";
            _brandRepositoryMock.Setup(mock => mock.GetByName(name, false)).Returns((Brand)null);

            // Act
            var result = _brandService.GetByName(name);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Brand not found", result.ErrorMessage);
        }

        [Fact]
        public void Update_ExistingBrand_ReturnsUpdatedBrand()
        {
            // Arrange
            var updateBrandDto = new UpdateBrandDto { Id = 1, Name = "New Nike" };
            var existingBrand = new Brand { Id = 1, Name = "Nike" };
            var updatedBrand = new Brand { Id = 1, Name = "New Nike" };

            _brandRepositoryMock.Setup(mock => mock.GetById(updateBrandDto.Id,false)).Returns(existingBrand);
            _brandRepositoryMock.Setup(mock => mock.Update(updateBrandDto.Id, It.IsAny<Brand>())).Returns(updatedBrand);
            _mapperMock.Setup(mock => mock.Map<Brand>(updateBrandDto)).Returns(updatedBrand);
            _mapperMock.Setup(mock => mock.Map<GetBrandByIdDto>(updatedBrand)).Returns(
                new GetBrandByIdDto { Id = 1, Name = "New Nike" });

            // Act
            var result = _brandService.Update(updateBrandDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(updatedBrand.Id, result.Result.Id);
            Assert.Equal(updatedBrand.Name, result.Result.Name);
        }

        [Fact]
        public void Update_NonExistingBrand_ReturnsError()
        {
            // Arrange
            var updateBrandDto = new UpdateBrandDto { Id = 1, Name = "New Nike" };
            _brandRepositoryMock.Setup(mock => mock.GetById(updateBrandDto.Id, false)).Returns((Brand)null);

            // Act
            var result = _brandService.Update(updateBrandDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Brand not found", result.ErrorMessage);
        }
    }
}
