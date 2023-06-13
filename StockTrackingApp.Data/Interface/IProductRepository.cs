using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data.Interface
{
    public interface IProductRepository
    {
        Product GetById(int id, bool withBrand = true, bool withCategory = true, bool withStocks = false);
        List<Product> Search(string? name, int? categoryId, int? brandId, double? minPrice, int? sizeId, int? colorId, bool withStocks = false, SortBy sortBy = SortBy.Default, int page = 0, int pageSize = -1);
        Product Add(Product product);
        Product Update(int id, Product product);
        Product Delete(int id);
    }
}
