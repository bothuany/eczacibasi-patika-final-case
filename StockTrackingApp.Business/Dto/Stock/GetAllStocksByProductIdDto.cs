using StockTrackingApp.Business.Dto.Color;
using StockTrackingApp.Business.Dto.Product;
using StockTrackingApp.Business.Dto.Size;

namespace StockTrackingApp.Business.Dto.Stock
{
    public class GetAllStocksByProductIdDto
    {
        public int Id { get; set; }
        public GetColorByIdDto Color { get; set; }
        public GetSizeByIdDto Size { get; set; }
        public int Quantity { get; set; }
        public GetProductByIdDto Product { get; set; }
    }
}
