using StockTrackingApp.Business.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Dto.Brand
{
    public class GetBrandByIdWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetAllProductsDto> Products { get; set; }
    }
}
