using MediatR;

namespace RealtimeNotifications.Models
{
    public class Order 
    {
        public int Id { get; set; }
        public string customerName { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
