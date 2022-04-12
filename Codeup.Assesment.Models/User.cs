namespace Codeup.Assesment.DTOs
{
    
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CreateOrderItemsDto> OrderItems { get; set; }

    }
    public class CreateOrderItemsDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
    public class UpdateOrderDto: CreateOrderDto
    {
        public int Id { get; set; }
    }
    public class GetOrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CreateOrderItemsDto> OrderItems { get; set; }

    }
    public class GetOrderItemsDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class Merchant
    {
        public string CountryCode { get; set; }
        public string MerchantName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Merchant Merchant { get; set; }
    }
    public enum ProductStatus
    {
        Active,
        InActive
    }
}