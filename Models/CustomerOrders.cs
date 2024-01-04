using System.ComponentModel.DataAnnotations;

namespace DD_System.Models
{
    public class CustomerOrders
    {
        [Key]
        public int C_ID { get; set; }
        public int P_ID { get; set; }
        public string? FristName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int Phonenumber { get; set; }
        public string? Address { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
