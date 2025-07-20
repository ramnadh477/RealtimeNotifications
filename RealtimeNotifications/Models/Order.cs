using MediatR;

namespace RealtimeNotifications.Models
{
    public class Order 
    {
        public int Id { get; set; }
        public required string customerName { get; set; }
        public required string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public required string Status { get; set; }
    }
}
