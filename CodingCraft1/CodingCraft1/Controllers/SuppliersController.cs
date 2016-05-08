using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CodingCraft1.Context;
using CodingCraft1.Models;

namespace CodingCraft1.Controllers
{
    [Authorize(Roles = "admin")]
    [RoutePrefix("api/suppliers")]
    public class SuppliersController : ApiController
    {
        private readonly MyContext _db = new MyContext();

        // GET: api/Suppliers
        public IQueryable<Supplier> GetSuppliers()
        {
            return _db.Suppliers;
        }

        // GET: api/Suppliers/5
        [ResponseType(typeof(Supplier))]
        public async Task<IHttpActionResult> GetSupplier(int id)
        {
            Supplier supplier = await _db.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // GET: api/Supplier/5/Products
        [Route("{supplierId}/products")]
        public async Task<IHttpActionResult> GetProductsPerSupplier(int supplierId)
        {
            var products = await _db.ProductsPerSuppliers
                                    .Include(x => x.Product)
                                    .Where(x => x.SupplierId == supplierId)
                                    .Select(x => new
                                    {
                                        Id = x.Product.Id,
                                        Description = x.Product.Description,
                                        SalePrice = x.Product.SalePrice,
                                        Price = x.Price,
                                        StockQuantity = x.Product.StockQuantity
                                    }).ToListAsync();

            return Ok(products);
        }

        // PUT: api/Suppliers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSupplier(int id, Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplier.Id)
            {
                return BadRequest();
            }

            _db.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Suppliers
        [ResponseType(typeof(Supplier))]
        public async Task<IHttpActionResult> PostSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Suppliers.Add(supplier);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = supplier.Id }, supplier);
        }

        // DELETE: api/Suppliers/5
        [ResponseType(typeof(Supplier))]
        public async Task<IHttpActionResult> DeleteSupplier(int id)
        {
            Supplier supplier = await _db.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _db.Suppliers.Remove(supplier);
            await _db.SaveChangesAsync();

            return Ok(supplier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierExists(int id)
        {
            return _db.Suppliers.Count(e => e.Id == id) > 0;
        }
    }
}