using System.ComponentModel.DataAnnotations;

namespace StockTrackingApp.Business.Dto.Size
{
    public class CreateSizeDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters")]
        public string Name { get; set; }
    }
}
