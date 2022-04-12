namespace Codeup.Assesment.Entities
{
    public class MerchantPeriod
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int CountryCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Merchant Merchant { get; set; }

    }
}
