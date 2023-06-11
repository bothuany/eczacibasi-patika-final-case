using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data.Entity
{
    [Table("Brands")]
    public class Brand : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
