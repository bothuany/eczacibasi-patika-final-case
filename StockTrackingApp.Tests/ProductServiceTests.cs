using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Moq;
using StockTrackingApp.Business;
using StockTrackingApp.Business.Dto.Brand;
using StockTrackingApp.Business.Dto.Category;
using StockTrackingApp.Business.Dto.Product;
using StockTrackingApp.Business.Dto.Stock;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using Xunit;

namespace StockTrackingApp.UnitTests.Business
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Add_ValidProduct_ReturnsAddedProduct()
        {
            // Arrange
            var createProductDto = new CreateProductDto
            {
                Name = "New Product",
                Description = "Product description",
                CategoryId = 1,
                BrandId = 1,
                Price = 19.99
            };

            var addedProduct = new Product
            {
                Id = 1,
                Name = "New Product",
                Description = "Product description",
                CategoryId = 1,
                BrandId = 1,
                Price = 19.99
            };

            _productRepositoryMock.Setup(mock => mock.Add(It.IsAny<Product>())).Returns(addedProduct);
            _mapperMock.Setup(mock => mock.Map<Product>(createProductDto)).Returns(addedProduct);
            _mapperMock.Setup(mock => mock.Map<GetProductByIdDto>(addedProduct)).Returns(
                new GetProductByIdDto
                {
                    Id = 1,
                    Name = "New Product",
                    Description = "Product description",
                    Category = new GetCategoryByIdDto { Id = 1, Name = "Category" },
                    Brand = new GetBrandByIdDto { Id = 1, Name = "Brand" },
                    Price = 19.99
                });

            // Act
            var result = _productService.Add(createProductDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(addedProduct.Id, result.Result.Id);
            Assert.Equal(addedProduct.Name, result.Result.Name);
            Assert.Equal(addedProduct.Description, result.Result.Description);
            Assert.Equal(addedProduct.CategoryId, result.Result.Category.Id);
            Assert.Equal(addedProduct.BrandId, result.Result.Brand.Id);
            Assert.Equal(addedProduct.Price, result.Result.Price);
        }

        [Fact]
        public void Add_NullProduct_ReturnsError()
        {
            // Arrange
            CreateProductDto createProductDto = null;

            // Act
            var result = _productService.Add(createProductDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Something went wrong", result.ErrorMessage);
        }

        [Fact]
        public void Delete_NonExistingProduct_ReturnsError()
        {
            // Arrange
            var id = 1;
            _productRepositoryMock.Setup(mock => mock.GetById(id, true, true, true)).Returns((Product)null);

            // Act
            var result = _productService.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Product not found", result.ErrorMessage);
        }

        [Fact]
        public void GetById_ExistingProduct_ReturnsProductWithId()
        {
            // Arrange
            var id = 1;
            var existingProduct = new Product
            {
                Id = id,
                Name = "Product A",
                Price = 10.99,
                BrandId = 1,
                CategoryId = 1
            };

            _productRepositoryMock.Setup(mock => mock.GetById(id,true,true,true)).Returns(existingProduct);
            _mapperMock.Setup(mock => mock.Map<GetProductByIdWithStocksDto>(existingProduct)).Returns(
                new GetProductByIdWithStocksDto
                {
                    Id = id,
                    Name = "Product A",
                    Price = 10.99,
                    Category = new GetCategoryByIdDto { Id = 1, Name = "Category" },
                    Brand = new GetBrandByIdDto { Id = 1, Name = "Brand" },
                    Stocks = new List<GetAllStocksDto>()
                });

            // Act
            var result = _productService.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(existingProduct.Id, result.Result.Id);
            Assert.Equal(existingProduct.Name, result.Result.Name);
            Assert.Equal(existingProduct.Price, result.Result.Price);
            Assert.Equal(existingProduct.BrandId, result.Result.Brand.Id);
            Assert.Equal(existingProduct.CategoryId, result.Result.Category.Id);
            Assert.Empty(result.Result.Stocks);
        }

        [Fact]
        public void GetById_NonExistingProduct_ReturnsError()
        {
            // Arrange
            var id = 1;
            _productRepositoryMock.Setup(mock => mock.GetById(id,true,true,true)).Returns((Product)null);

            // Act
            var result = _productService.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Product not found", result.ErrorMessage);
        }

        [Fact]
        public void Search_WithValidParameters_ReturnsMatchingProducts()
        {
            // Arrange
            var name = "Product";
            var categoryId = 1;
            var brandId = 1;
            var minPrice = 0.99;
            var sizeId = 1;
            var colorId = 1;
            var withStocks = true;
            var page = 0;
            var pageSize = -1;

            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Product A",
                    Price = 10.99,
                    BrandId = 1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Product B",
                    Price = 19.99,
                    BrandId = 1,
                    CategoryId = 2
                }
            };

            _productRepositoryMock.Setup(mock => mock.Search(name, categoryId, brandId, minPrice, sizeId, colorId, withStocks, page, pageSize))
                .Returns(products);
            _mapperMock.Setup(mock => mock.Map<List<GetAllProductsWithStocksDto>>(products)).Returns(
                new List<GetAllProductsWithStocksDto>
                {
                    new GetAllProductsWithStocksDto
                    {
                        Id = 1,
                        Name = "Product A",
                        Price = 10.99,
                        Category = new GetCategoryByIdDto { Id = 1, Name = "Category" },
                        Brand = new GetBrandByIdDto { Id = 1, Name = "Brand" }
                    },
                    new GetAllProductsWithStocksDto
                    {
                        Id = 2,
                        Name = "Product B",
                        Price = 19.99,
                        Category = new GetCategoryByIdDto { Id = 2, Name = "Category" },
                        Brand = new GetBrandByIdDto { Id = 1, Name = "Brand" }
                    }
                });

            // Act
            var result = _productService.Search(name, categoryId, brandId, minPrice, sizeId, colorId, withStocks, page, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeed);
            Assert.Equal(products.Count, result.Result.Count);
            Assert.Equal(products[0].Id, result.Result[0].Id);
            Assert.Equal(products[0].Name, result.Result[0].Name);
            Assert.Equal(products[0].Price, result.Result[0].Price);
            Assert.Equal(products[0].BrandId, result.Result[0].Brand.Id);
            Assert.Equal(products[0].CategoryId, result.Result[0].Category.Id);
            Assert.Equal(products[1].Id, result.Result[1].Id);
            Assert.Equal(products[1].Name, result.Result[1].Name);
            Assert.Equal(products[1].Price, result.Result[1].Price);
            Assert.Equal(products[1].BrandId, result.Result[1].Brand.Id);
            Assert.Equal(products[1].CategoryId, result.Result[1].Category.Id);
        }

        [Fact]
        public void Search_WithInvalidParameters_ReturnsError()
        {
            // Arrange
            string name = null;
            int? categoryId = null;
            int? brandId = null;
            double? minPrice = null;
            int? sizeId = null;
            int? colorId = null;
            bool withStocks = true;
            int page = 0;
            int pageSize = -1;

            _productRepositoryMock.Setup(mock => mock.Search(name, categoryId, brandId, minPrice, sizeId, colorId, withStocks, page, pageSize))
                .Returns((List<Product>)null);

            // Act
            var result = _productService.Search(name, categoryId, brandId, minPrice, sizeId, colorId, withStocks, page, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeed);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.ErrorCode);
            Assert.Null(result.Result);
            Assert.Equal("Something went wrong", result.ErrorMessage);
        }
    }
}
