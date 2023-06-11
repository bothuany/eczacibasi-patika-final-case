using System.ComponentModel.DataAnnotations;

namespace StockTrackingApp.Business.Dto.Stock
{
    public class UpdateStockDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive value")]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
