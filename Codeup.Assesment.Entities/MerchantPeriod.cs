namespace Codeup.Assesment.Entities
{
    public class MerchantPeriod
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int CountryCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Merchant Merchant1 { get; set; }
        public virtual Merchant Merchant2 { get; set; }

    }
}
