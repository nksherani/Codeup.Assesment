using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codeup.Assesment.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }

    }
}
