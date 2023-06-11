using AutoMapper;
using Moq;
using StockTrackingApp.Business.Dto.Color;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Business;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using Xunit;
using System.Net;

namespace StockTrackingApp.Business.Tests
{
    public class ColorServiceTests
    {
        private readonly IColorService _colorService;
        private readonly Mock<IColorRepository> _colorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public ColorServiceTests()
        {
            _colorRepositoryMock = new Mock<IColorRepository>();
            _mapperMock = new Mock<IMapper>();
            _colorService = new ColorService(_colorRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsListOfColors()
        {
            // Arrange
            var colors = new List<Color>
            {
                new Color { Id = 1, Name = "Red" },
                new Color { Id = 2, Name = "Blue" },
                new Color { Id = 3, Name = "Green" }
            };

            _colorRepositoryMock.Setup(mock => mock.GetAll()).Returns(colors);
            _mapperMock.Setup(mock => mock.Map<List<GetAllColorsDto>>(colors)).Returns(
                new List<GetAllColorsDto>
                {
            new GetAllColorsDto { Id = 1, Name = "Red" },
            new GetAllColorsDto { Id = 2, Name = "Blue" },
            new GetAllColorsDto { Id = 3, Name = "Green" }
                });

            // Act
            var result = _colorService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(colors.Count, result.Result.Count);
        }

        [Fact]
        public void Add_ValidColor_ReturnsAddedColor()
        {
            // Arrange
            var createColorDto = new CreateColorDto { Name = "Red" };
            var color = new Color { Id = 1, Name = "Red" };

            _colorRepositoryMock.Setup(mock => mock.GetByName(createColorDto.Name)).Returns((Color)null);
            _colorRepositoryMock.Setup(mock => mock.Add(It.IsAny<Color>())).Returns(color);
            _mapperMock.Setup(mock => mock.Map<Color>(createColorDto)).Returns(color);
            _mapperMock.Setup(mock => mock.Map<GetColorByIdDto>(color)).Returns(
                new GetColorByIdDto { Id = 1, Name = "Red" });

            // Act
            var result = _colorService.Add(createColorDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(color.Id, result.Result.Id);
            Assert.Equal(color.Name, result.Result.Name);
        }

        [Fact]
        public void Add_ExistingColor_ReturnsError()
        {
            // Arrange
            var createColorDto = new CreateColorDto { Name = "Red" };
            var existingColor = new Color { Id = 1, Name = "Red" };

            _colorRepositoryMock.Setup(mock => mock.GetByName(createColorDto.Name)).Returns(existingColor);

            // Act
            var result = _colorService.Add(createColorDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Color already exists", result.ErrorMessage);
        }

        [Fact]
        public void Delete_ExistingColor_ReturnsDeletedColor()
        {
            // Arrange
            var id = 1;
            var existingColor = new Color { Id = id, Name = "Red" };

            _colorRepositoryMock.Setup(mock => mock.GetById(id)).Returns(existingColor);
            _colorRepositoryMock.Setup(mock => mock.Delete(id)).Returns(existingColor);
            _mapperMock.Setup(mock => mock.Map<GetColorByIdDto>(existingColor)).Returns(
                new GetColorByIdDto { Id = id, Name = "Red" });

            // Act
            var result = _colorService.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(existingColor.Id, result.Result.Id);
            Assert.Equal(existingColor.Name, result.Result.Name);
        }

        [Fact]
        public void Delete_NonExistingColor_ReturnsError()
        {
            // Arrange
            var id = 1;
            _colorRepositoryMock.Setup(mock => mock.GetById(id)).Returns((Color)null);

            // Act
            var result = _colorService.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Color not found", result.ErrorMessage);
        }
    }
}