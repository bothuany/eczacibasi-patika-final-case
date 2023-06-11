using AutoMapper;
using Moq;
using StockTrackingApp.Business.Dto.Size;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Business;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using Xunit;

namespace StockTrackingApp.Business.Tests
{
    public class SizeServiceTests
    {
        private readonly ISizeService _sizeService;
        private readonly Mock<ISizeRepository> _sizeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public SizeServiceTests()
        {
            _sizeRepositoryMock = new Mock<ISizeRepository>();
            _mapperMock = new Mock<IMapper>();

            _sizeService = new SizeService(_sizeRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsListOfSizes()
        {
            // Arrange
            var sizes = new List<Size>
            {
                new Size { Id = 1, Name = "S" },
                new Size { Id = 2, Name = "M" },
                new Size { Id = 3, Name = "L" }
            };

            _sizeRepositoryMock.Setup(mock => mock.GetAll()).Returns(sizes);
            _mapperMock.Setup(mock => mock.Map<List<GetAllSizesDto>>(sizes)).Returns(
                new List<GetAllSizesDto>
                {
                    new GetAllSizesDto { Id = 1, Name = "S" },
                    new GetAllSizesDto { Id = 2, Name = "M" },
                    new GetAllSizesDto { Id = 3, Name = "L" }
                });

            // Act
            var result = _sizeService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(sizes.Count, result.Result.Count);
        }

        [Fact]
        public void GetById_ExistingSizeId_ReturnsSize()
        {
            // Arrange
            var sizeId = 1;
            var size = new Size { Id = sizeId, Name = "S" };

            _sizeRepositoryMock.Setup(mock => mock.GetById(sizeId)).Returns(size);
            _mapperMock.Setup(mock => mock.Map<GetSizeByIdDto>(size)).Returns(
                new GetSizeByIdDto { Id = sizeId, Name = "S" });

            // Act
            var result = _sizeService.GetById(sizeId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(sizeId, result.Result.Id);
        }

        [Fact]
        public void GetById_NonExistingSizeId_ReturnsNotFound()
        {
            // Arrange
            var sizeId = 100;

            _sizeRepositoryMock.Setup(mock => mock.GetById(sizeId)).Returns((Size)null);

            // Act
            var result = _sizeService.GetById(sizeId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal("Size not found", result.ErrorMessage);
        }

        [Fact]
        public void Add_NewSize_ReturnsAddedSize()
        {
            // Arrange
            var newSizeDto = new CreateSizeDto { Name = "XL" };
            var addedSize = new Size { Id = 1, Name = "XL" };

            _sizeRepositoryMock.Setup(mock => mock.GetByName(newSizeDto.Name)).Returns((Size)null);
            _sizeRepositoryMock.Setup(mock => mock.Add(It.IsAny<Size>())).Returns(addedSize);
            _mapperMock.Setup(mock => mock.Map<Size>(newSizeDto)).Returns(addedSize);
            _mapperMock.Setup(mock => mock.Map<GetSizeByIdDto>(addedSize)).Returns(
                new GetSizeByIdDto { Id = addedSize.Id, Name = addedSize.Name });

            // Act
            var result = _sizeService.Add(newSizeDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(addedSize.Id, result.Result.Id);
        }

        [Fact]
        public void Add_ExistingSize_ReturnsBadRequest()
        {
            // Arrange
            var existingSizeName = "S";
            var newSizeDto = new CreateSizeDto { Name = existingSizeName };
            var existingSize = new Size { Id = 1, Name = existingSizeName };

            _sizeRepositoryMock.Setup(mock => mock.GetByName(existingSizeName)).Returns(existingSize);

            // Act
            var result = _sizeService.Add(newSizeDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal("Size already exists", result.ErrorMessage);
        }

        [Fact]
        public void Update_ExistingSize_ReturnsUpdatedSize()
        {
            // Arrange
            var sizeId = 1;
            var updateSizeDto = new UpdateSizeDto { Id = sizeId, Name = "M" };
            var existingSize = new Size { Id = sizeId, Name = "S" };
            var updatedSize = new Size { Id = sizeId, Name = "M" };

            _sizeRepositoryMock.Setup(mock => mock.GetById(sizeId)).Returns(existingSize);
            _sizeRepositoryMock.Setup(mock => mock.Update(sizeId, It.IsAny<Size>())).Returns(updatedSize);
            _mapperMock.Setup(mock => mock.Map<Size>(updateSizeDto)).Returns(updatedSize);
            _mapperMock.Setup(mock => mock.Map<GetSizeByIdDto>(updatedSize)).Returns(
                new GetSizeByIdDto { Id = updatedSize.Id, Name = updatedSize.Name });

            // Act
            var result = _sizeService.Update(updateSizeDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(updatedSize.Id, result.Result.Id);
            Assert.Equal(updatedSize.Name, result.Result.Name);
        }

        [Fact]
        public void Update_NonExistingSize_ReturnsNotFound()
        {
            // Arrange
            var sizeId = 100;
            var updateSizeDto = new UpdateSizeDto { Id = sizeId, Name = "M" };

            _sizeRepositoryMock.Setup(mock => mock.GetById(sizeId)).Returns((Size)null);

            // Act
            var result = _sizeService.Update(updateSizeDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal("Size not found", result.ErrorMessage);
        }

        [Fact]
        public void Delete_ExistingSizeId_ReturnsDeletedSize()
        {
            // Arrange
            var sizeId = 1;
            var existingSize = new Size { Id = sizeId, Name = "S" };

            _sizeRepositoryMock.Setup(mock => mock.GetById(sizeId)).Returns(existingSize);
            _sizeRepositoryMock.Setup(mock => mock.Delete(sizeId)).Returns(existingSize);
            _mapperMock.Setup(mock => mock.Map<GetSizeByIdDto>(existingSize)).Returns(
                new GetSizeByIdDto { Id = existingSize.Id, Name = existingSize.Name });

            // Act
            var result = _sizeService.Delete(sizeId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(existingSize.Id, result.Result.Id);
            Assert.Equal(existingSize.Name, result.Result.Name);
        }

        [Fact]
        public void Delete_NonExistingSizeId_ReturnsNotFound()
        {
            // Arrange
            var sizeId = 100;

            _sizeRepositoryMock.Setup(mock => mock.GetById(sizeId)).Returns((Size)null);

            // Act
            var result = _sizeService.Delete(sizeId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal("Size not found", result.ErrorMessage);
        }
    }
}
