using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeup.Assesment.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [Timestamp]
        public byte[] CreatedAt { get; set; }
        public int CountryCode { get; set; }
        public virtual Country Country { get; set; }
        public virtual List<Merchant> Merchants { get; set; }

    }
}
