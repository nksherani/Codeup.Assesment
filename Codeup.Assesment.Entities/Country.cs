using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codeup.Assesment.Entities
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Name { get; set; }
        public string ContinentName { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Merchant> Merchants { get; set; }

    }
}
