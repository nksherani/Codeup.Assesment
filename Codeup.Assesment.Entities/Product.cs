namespace Codeup.Assesment.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MerchantId { get; set; }
        public int Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Merchant Merchant { get; set; }

    }
}
