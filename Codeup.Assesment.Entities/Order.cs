namespace Codeup.Assesment.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }

    }
}
