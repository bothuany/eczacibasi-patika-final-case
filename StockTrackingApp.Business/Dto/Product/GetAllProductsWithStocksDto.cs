using StockTrackingApp.Business.Dto.Brand;
using StockTrackingApp.Business.Dto.Category;
using StockTrackingApp.Business.Dto.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Dto.Product
{
    public class GetAllProductsWithStocksDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GetCategoryByIdDto Category { get; set; }
        public GetBrandByIdDto Brand { get; set; }
        public double Price { get; set; }
        public List<GetAllStocksWithoutProductDto> Stocks { get; set; }
    }
}
