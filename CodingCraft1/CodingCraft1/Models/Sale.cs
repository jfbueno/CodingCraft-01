using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraft1.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string ConsumerId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime Date { get; set; }

        public virtual List<SaleItems> Items { get; set; }
        [ForeignKey("ConsumerId")]
        public virtual IdentityUser Consumer { get; set; }
    }
}
