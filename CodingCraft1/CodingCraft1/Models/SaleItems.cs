using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraft1.Models
{
    public class SaleItems
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SaleId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product BaseProduct { get; set; }
        [ForeignKey("SaleId")]
        public virtual Sale Sale { get; set; }
    }
}
