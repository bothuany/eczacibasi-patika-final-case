using StockTrackingApp.Business.Dto.Color;
using StockTrackingApp.Business.Dto.Product;
using StockTrackingApp.Business.Dto.Size;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Dto.Stock
{
    public class GetAllStocksWithoutProductDto
    {
        public int Id { get; set; }
        public GetColorByIdDto Color { get; set; }
        public GetSizeByIdDto Size { get; set; }
        public int Quantity { get; set; }
    }
}
