using Microsoft.EntityFrameworkCore;

namespace DD_System.Models
{
    public class AddFootwearContext : DbContext
    {
        public AddFootwearContext(DbContextOptions<AddFootwearContext> options) : base(options)
        {

        }
        public DbSet<Footwear> Footwear { get; set; }
        public DbSet<ProductOrders> ProductOrders { get; set; }
        public DbSet<WebOrders> WebOrders { get; set; }
        public DbSet<CustomerOrders> CustomerOrders { get; set; }
    }
}
 