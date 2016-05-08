using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraft1.Models
{
    [Table("ProductsPerSuppliers")]
    public class ProductPerSupplier
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
    }
}
