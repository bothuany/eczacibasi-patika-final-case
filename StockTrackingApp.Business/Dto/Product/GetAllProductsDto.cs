using StockTrackingApp.Business.Dto.Brand;
using StockTrackingApp.Business.Dto.Category;
using StockTrackingApp.Business.Dto.Stock;
using System.Collections.Generic;

namespace StockTrackingApp.Business.Dto.Product
{
    public class GetAllProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GetCategoryByIdDto Category { get; set; }
        public GetBrandByIdDto Brand { get; set; }
        public double Price { get; set; }
    }
}
