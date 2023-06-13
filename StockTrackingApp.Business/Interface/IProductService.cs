using StockTrackingApp.Business.Dto.Product;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Interface
{
    public interface IProductService
    {
        ServiceResult<GetProductByIdWithStocksDto> GetById(int id, bool withCategory = true, bool withBrand = true, bool withStocks = true);
        ServiceResult<List<GetAllProductsWithStocksDto>> Search(string name, int? categoryId, int? brandId, double? minPrice, int? sizeId, int? colorId, bool withStocks = true, string sortBy = "Default", int page = 0, int pageSize = -1);
        ServiceResult<GetProductByIdDto> Add(CreateProductDto createProductDto);
        ServiceResult<GetProductByIdDto> Update(UpdateProductDto updateProductDto);
        ServiceResult<GetProductByIdDto> Delete(int id);
    }
}
