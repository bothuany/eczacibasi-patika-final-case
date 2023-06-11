using Microsoft.EntityFrameworkCore;
using StockTrackingApp.Data.Entity;

namespace StockTrackingApp.Data
{
    public class StockTrackingContext : DbContext
    {

        public StockTrackingContext(DbContextOptions<StockTrackingContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Stock> Stocks { get; set; }


    }
}
