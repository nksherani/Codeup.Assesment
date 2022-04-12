using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codeup.Assesment.Entities
{
    public class Merchant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CountryCode { get; set; }
        public string MerchantName { get; set; }
        public string CreatedAt { get; set; }
        public int AdminId { get; set; }
        public virtual Country Country { get; set; }
        public virtual User Admin { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual MerchantPeriod MerchantPeriod1 { get; set; }
        public virtual MerchantPeriod MerchantPeriod2 { get; set; }

    }
}
