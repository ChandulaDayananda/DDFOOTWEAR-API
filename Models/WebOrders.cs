using System.ComponentModel.DataAnnotations;

namespace DD_System.Models
{
    public class WebOrders
    {
        [Key]
        public int W_ID { get; set; }
        public string? ProductName { get; set; }
        public string? Category { get; set; }
        public string? Gender { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public int IsActive { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
