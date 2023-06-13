using AutoMapper;
using StockTrackingApp.Business.Dto.Product;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Enums;
using StockTrackingApp.Data.Interface;
using System;
using System.Collections.Generic;
using System.Net;

namespace StockTrackingApp.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        public ServiceResult<GetProductByIdDto> Add(CreateProductDto createProductDto)
        {
            Product addedProduct = _productRepository.Add(_mapper.Map<Product>(createProductDto));

            if (addedProduct is null)
            {
                return ServiceResult<GetProductByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetProductByIdDto>.Success(_mapper.Map<GetProductByIdDto>(addedProduct));
        }

        public ServiceResult<GetProductByIdDto> Delete(int id)
        {
            var product = _productRepository.GetById(id);

            if (product is null)
            {
                return ServiceResult<GetProductByIdDto>.Failed(null, "Product not found", (int)HttpStatusCode.NotFound);
            }

            Product deletedProduct = _productRepository.Delete(id);

            if (deletedProduct is null)
            {
                return ServiceResult<GetProductByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetProductByIdDto>.Success(_mapper.Map<GetProductByIdDto>(deletedProduct));
        }

        public ServiceResult<GetProductByIdWithStocksDto> GetById(int id, bool withCategory = true, bool withBrand = true, bool withStocks = true)
        {
            var product = _productRepository.GetById(id, withCategory, withBrand, withStocks);

            if (product is null)
            {
                return ServiceResult<GetProductByIdWithStocksDto>.Failed(null, "Product not found", (int)HttpStatusCode.NotFound);
            }

            var mappedProduct = _mapper.Map<GetProductByIdWithStocksDto>(product);

            return ServiceResult<GetProductByIdWithStocksDto>.Success(mappedProduct);
        }

        public ServiceResult<List<GetAllProductsWithStocksDto>> Search(string? name, int? categoryId, int? brandId, double? minPrice, int? sizeId, int? colorId, bool withStocks = true, string sortBy = "Default", int page = 0, int pageSize = -1)
        {
            var products = _productRepository.Search(name, categoryId, brandId, minPrice, sizeId, colorId, withStocks, ConvertToSortBy(sortBy), page, pageSize);

            if (products is null)
            {
                return ServiceResult<List<GetAllProductsWithStocksDto>>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            var mappedProducts = _mapper.Map<List<GetAllProductsWithStocksDto>>(products);

            return ServiceResult<List<GetAllProductsWithStocksDto>>.Success(mappedProducts);
        }

        public ServiceResult<GetProductByIdDto> Update(UpdateProductDto updateProductDto)
        {
            var product = _productRepository.GetById(updateProductDto.Id);

            if (product is null)
            {
                return ServiceResult<GetProductByIdDto>.Failed(null, "Product not found", (int)HttpStatusCode.NotFound);
            }

            var updatedProduct = _productRepository.Update(updateProductDto.Id, _mapper.Map<Product>(updateProductDto));

            if (updatedProduct is null)
            {
                return ServiceResult<GetProductByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetProductByIdDto>.Success(_mapper.Map<GetProductByIdDto>(updatedProduct));
        }


        private SortBy ConvertToSortBy(string sortBy)
        {
            switch (sortBy.ToLower())
            {
                case "priceascending":
                    return SortBy.PriceAscending;
                case "pricedescending":
                    return SortBy.PriceDescending;
                case "name":
                    return SortBy.Name;
                default:
                    return SortBy.Default;
            }
        }

    }
}
