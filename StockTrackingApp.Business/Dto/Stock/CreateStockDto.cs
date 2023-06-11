using System.ComponentModel.DataAnnotations;

namespace StockTrackingApp.Business.Dto.Stock
{
    public class CreateStockDto
    {
        public int ColorId { get; set; }
        public int SizeId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive value")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }
    }
}
