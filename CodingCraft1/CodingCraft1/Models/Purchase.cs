using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCraft1.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime Payday { get; set; }
        public DateTime RegistrationDate { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
        
        public virtual IEnumerable<PurchaseItems> Items { get; set; }

    }
}
