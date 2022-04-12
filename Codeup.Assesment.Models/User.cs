namespace Codeup.Assesment.DTOs
{

    public class CreateOrderDto
    {
        public List<CreateOrderItemDto> OrderItems { get; set; }

    }
    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
    public class UpdateOrderDto : CreateOrderDto
    {
        public int Id { get; set; }
    }
    public class GetOrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<GetOrderItemsDto> OrderItems { get; set; }

    }
    public class GetOrderItemsDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductDto Product { get; set; }
    }
    public class MerchantDto
    {
        public int Id { get; set; }
        public int CountryCode { get; set; }
        public string MerchantName { get; set; }
        public string CreatedAt { get; set; }
    }
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public MerchantDto Merchant { get; set; }
    }
    public enum ProductStatus
    {
        Active,
        InActive
    }
}