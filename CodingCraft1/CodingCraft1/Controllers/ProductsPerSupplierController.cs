using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CodingCraft1.Context;
using CodingCraft1.Models;

namespace CodingCraft1.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsPerSupplierController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/ProductsPerSupplier
        public IQueryable<ProductPerSupplier> GetProductsPerSuppliers()
        {
            return db.ProductsPerSuppliers;
        }

        // GET: api/ProductsPerSupplier/5
        [ResponseType(typeof(ProductPerSupplier))]
        public async Task<IHttpActionResult> GetProductPerSupplier(int id)
        {
            ProductPerSupplier productPerSupplier = await db.ProductsPerSuppliers.FindAsync(id);
            if (productPerSupplier == null)
            {
                return NotFound();
            }

            return Ok(productPerSupplier);
        }

        // PUT: api/ProductsPerSupplier/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductPerSupplier(int id, ProductPerSupplier productPerSupplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productPerSupplier.Id)
            {
                return BadRequest();
            }

            db.Entry(productPerSupplier).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPerSupplierExists(id))
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

        // POST: api/ProductsPerSupplier
        [ResponseType(typeof(ProductPerSupplier))]
        public async Task<IHttpActionResult> PostProductPerSupplier(ProductPerSupplier productPerSupplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductsPerSuppliers.Add(productPerSupplier);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productPerSupplier.Id }, productPerSupplier);
        }

        // DELETE: api/ProductsPerSupplier/5
        [ResponseType(typeof(ProductPerSupplier))]
        public async Task<IHttpActionResult> DeleteProductPerSupplier(int id)
        {
            ProductPerSupplier productPerSupplier = await db.ProductsPerSuppliers.FindAsync(id);
            if (productPerSupplier == null)
            {
                return NotFound();
            }

            db.ProductsPerSuppliers.Remove(productPerSupplier);
            await db.SaveChangesAsync();

            return Ok(productPerSupplier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductPerSupplierExists(int id)
        {
            return db.ProductsPerSuppliers.Count(e => e.Id == id) > 0;
        }
    }
}