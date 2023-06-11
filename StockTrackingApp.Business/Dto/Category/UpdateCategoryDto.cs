using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Dto.Category
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }
    }
}
