using System.ComponentModel.DataAnnotations;

namespace StockTrackingApp.Business.Dto.Size
{
    public class UpdateSizeDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters")]
        public string Name { get; set; }
    }
}
