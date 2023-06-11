using AutoMapper;
using StockTrackingApp.Business.Dto.Brand;
using StockTrackingApp.Business.Dto.Category;
using StockTrackingApp.Business.Dto.Color;
using StockTrackingApp.Business.Dto.Product;
using StockTrackingApp.Business.Dto.Size;
using StockTrackingApp.Business.Dto.Stock;
using StockTrackingApp.Data.Entity;

namespace StockTrackingApp.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Product DTOs mapping
            CreateMap<Product, Product>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product,GetProductByIdDto>().ReverseMap();
            CreateMap<Product, GetProductByIdDto>().ReverseMap();
            CreateMap<Product, GetAllProductsDto>().ReverseMap();
            CreateMap<Product, GetProductByIdWithStocksDto>().ReverseMap();
            CreateMap<Product, GetAllProductsWithStocksDto>().ReverseMap();

            //Stock DTOs mapping
            CreateMap<CreateStockDto, Stock>();
            CreateMap<UpdateStockDto, Stock>();
            CreateMap<Stock, GetStockByIdDto>().ReverseMap();
            CreateMap<Stock, GetAllStocksDto>().ReverseMap();
            CreateMap<Stock, GetAllStocksWithoutProductDto>().ReverseMap();
            CreateMap<Stock, GetAllStocksByProductIdDto>().ReverseMap();

            //Category DTOs mapping
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();
            CreateMap<Category, GetAllCategoriesDto>().ReverseMap();
            CreateMap<Category,GetCategoryByIdWithProductsDto>().ReverseMap();

            //Brand DTOs mapping
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<UpdateBrandDto, Brand>();
            CreateMap<Brand, GetBrandByIdDto>().ReverseMap();
            CreateMap<Brand, GetAllBrandsDto>().ReverseMap();
            CreateMap<Brand, GetBrandByIdWithProductsDto>().ReverseMap();

            //Size DTOs mapping
            CreateMap<CreateSizeDto, Size>();
            CreateMap<UpdateSizeDto, Size>();
            CreateMap<Size, GetSizeByIdDto>().ReverseMap();
            CreateMap<Size, GetAllSizesDto>().ReverseMap();

            //Color DTOs mapping
            CreateMap<CreateColorDto, Color>();
            CreateMap<UpdateColorDto, Color>();
            CreateMap<Color, GetColorByIdDto>().ReverseMap();
            CreateMap<Color, GetAllColorsDto>().ReverseMap();
        }
    }
}
