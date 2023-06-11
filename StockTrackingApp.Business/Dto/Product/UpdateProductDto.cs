using System.ComponentModel.DataAnnotations;

namespace StockTrackingApp.Business.Dto.Product
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public double Price { get; set; }
    }
}
