using System.ComponentModel.DataAnnotations;

namespace StockTrackingApp.Business.Dto.Color
{
    public class CreateColorDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }
    }
}
