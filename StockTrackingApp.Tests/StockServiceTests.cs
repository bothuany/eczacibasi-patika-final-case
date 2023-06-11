using Xunit;
using Moq;
using AutoMapper;
using StockTrackingApp.Business;
using StockTrackingApp.Business.Dto.Stock;
using StockTrackingApp.Business.Dto.Color;
using StockTrackingApp.Business.Dto.Size;
using StockTrackingApp.Business.Dto.Product;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using System.Net;

namespace StockTrackingApp.Tests.Business
{
    public class StockServiceTests
    {
        private readonly Mock<IStockRepository> _stockRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly StockService _stockService;

        public StockServiceTests()
        {
            _stockRepositoryMock = new Mock<IStockRepository>();
            _mapperMock = new Mock<IMapper>();
            _stockService = new StockService(_stockRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Add_ValidStock_ReturnsAddedStock()
        {
            // Arrange
            var createStockDto = new CreateStockDto
            {
                ProductId = 1,
                SizeId = 1,
                ColorId = 1,
                Quantity = 10
            };

            var addedStock = new Stock
            {
                Id = 1,
                ProductId = createStockDto.ProductId,
                SizeId = createStockDto.SizeId,
                ColorId = createStockDto.ColorId,
                Quantity = createStockDto.Quantity
            };

            var mappedStockDto = new GetStockByIdDto
            {
                Id = addedStock.Id,
                Color = new GetColorByIdDto { Id = createStockDto.ColorId },
                Size = new GetSizeByIdDto { Id = createStockDto.SizeId },
                Quantity = addedStock.Quantity,
                Product = new GetProductByIdDto { Id = createStockDto.ProductId }
            };

            _stockRepositoryMock.Setup(mock => mock.Add(It.IsAny<Stock>())).Returns(addedStock);
            _mapperMock.Setup(mock => mock.Map<GetStockByIdDto>(addedStock)).Returns(mappedStockDto);

            // Act
            var result = _stockService.Add(createStockDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(mappedStockDto.Id, result.Result.Id);
            Assert.Equal(mappedStockDto.Color.Id, result.Result.Color.Id);
            Assert.Equal(mappedStockDto.Size.Id, result.Result.Size.Id);
            Assert.Equal(mappedStockDto.Quantity, result.Result.Quantity);
            Assert.Equal(mappedStockDto.Product.Id, result.Result.Product.Id);
        }

        [Fact]
        public void Add_InvalidStock_ReturnsFailedResult()
        {
            // Arrange
            var createStockDto = new CreateStockDto
            {
                ProductId = 1,
                SizeId = 1,
                ColorId = 1,
                Quantity = 10
            };

            _stockRepositoryMock.Setup(mock => mock.Add(It.IsAny<Stock>())).Returns((Stock)null);

            // Act
            var result = _stockService.Add(createStockDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Null(result.Result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.ErrorCode);
            Assert.Equal("Something went wrong", result.ErrorMessage);
        }

        [Fact]
        public void Delete_ExistingStock_ReturnsDeletedStock()
        {
            // Arrange
            var id = 1;
            var existingStock = new Stock
            {
                Id = id,
                ProductId = 1,
                SizeId = 1,
                ColorId = 1,
                Quantity = 10
            };

            _stockRepositoryMock.Setup(mock => mock.GetById(id)).Returns(existingStock);
            _stockRepositoryMock.Setup(mock => mock.Delete(id)).Returns(existingStock);

            var mappedStockDto = new GetStockByIdDto
            {
                Id = existingStock.Id,
                Color = new GetColorByIdDto { Id = existingStock.ColorId },
                Size = new GetSizeByIdDto { Id = existingStock.SizeId },
                Quantity = existingStock.Quantity,
                Product = new GetProductByIdDto { Id = existingStock.ProductId }
            };

            _mapperMock.Setup(mock => mock.Map<GetStockByIdDto>(existingStock)).Returns(mappedStockDto);

            // Act
            var result = _stockService.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(mappedStockDto.Id, result.Result.Id);
            Assert.Equal(mappedStockDto.Color.Id, result.Result.Color.Id);
            Assert.Equal(mappedStockDto.Size.Id, result.Result.Size.Id);
            Assert.Equal(mappedStockDto.Quantity, result.Result.Quantity);
            Assert.Equal(mappedStockDto.Product.Id, result.Result.Product.Id);
        }

        [Fact]
        public void Delete_NonExistingStock_ReturnsFailedResult()
        {
            // Arrange
            var id = 1;

            _stockRepositoryMock.Setup(mock => mock.GetById(id)).Returns((Stock)null);

            // Act
            var result = _stockService.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Null(result.Result);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
            Assert.Equal("Stock not found", result.ErrorMessage);
        }

        [Fact]
        public void UpdateQuantity_ExistingStock_ReturnsUpdatedStock()
        {
            // Arrange
            var stockId = 1;
            var quantityChange = 5;

            var existingStock = new Stock
            {
                Id = stockId,
                ProductId = 1,
                SizeId = 1,
                ColorId = 1,
                Quantity = 10
            };

            var updatedStock = new Stock
            {
                Id = stockId,
                ProductId = 1,
                SizeId = 1,
                ColorId = 1,
                Quantity = existingStock.Quantity + quantityChange
            };

            _stockRepositoryMock.Setup(mock => mock.GetById(stockId)).Returns(existingStock);
            _stockRepositoryMock.Setup(mock => mock.UpdateQuantity(stockId, quantityChange)).Returns(updatedStock);

            var mappedStockDto = new GetStockByIdDto
            {
                Id = updatedStock.Id,
                Color = new GetColorByIdDto { Id = updatedStock.ColorId },
                Size = new GetSizeByIdDto { Id = updatedStock.SizeId },
                Quantity = updatedStock.Quantity,
                Product = new GetProductByIdDto { Id = updatedStock.ProductId }
            };

            _mapperMock.Setup(mock => mock.Map<GetStockByIdDto>(updatedStock)).Returns(mappedStockDto);

            // Act
            var result = _stockService.UpdateQuantity(new UpdateStockQuantityDto { Id = stockId, QuantityChange = quantityChange });

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(mappedStockDto.Id, result.Result.Id);
            Assert.Equal(mappedStockDto.Color.Id, result.Result.Color.Id);
            Assert.Equal(mappedStockDto.Size.Id, result.Result.Size.Id);
            Assert.Equal(mappedStockDto.Quantity, result.Result.Quantity);
            Assert.Equal(mappedStockDto.Product.Id, result.Result.Product.Id);
        }

    }
}
