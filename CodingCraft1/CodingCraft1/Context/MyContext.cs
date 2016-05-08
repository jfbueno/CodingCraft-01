using System.Data.Entity;
using CodingCraft1.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraft1.Context
{
    public class MyContext : IdentityDbContext<IdentityUser>
    {
        public MyContext() : base("CodingCraftDb2")
        {
        }

        public static MyContext Create()
        {
            return new MyContext();
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductPerSupplier> ProductsPerSuppliers { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItems> SaleItems { get; set; }

        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseItems> PurchaseItems { get; set; }
    }
}
