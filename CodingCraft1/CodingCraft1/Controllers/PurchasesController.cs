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
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using CodingCraft1.Context;
using CodingCraft1.Models;

namespace CodingCraft1.Controllers
{
    [Authorize(Roles = "admin")]
    public class PurchasesController : ApiController
    {
        private MyContext db = new MyContext();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Purchases
        public IQueryable<Purchase> GetPurchase()
        {
            return db.Purchase.Include(p => p.Supplier).OrderByDescending(p => p.PurchaseDate);
        }

        // GET: api/Purchases/5
        [ResponseType(typeof(Purchase))]
        public async Task<IHttpActionResult> GetPurchase(int id)
        {
            Purchase purchase = await db.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/Purchases/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPurchase(int id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.Id)
            {
                return BadRequest();
            }

            db.Entry(purchase).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/Purchases
        [ResponseType(typeof(Purchase))]
        public async Task<IHttpActionResult> PostPurchase(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //purchase.SupplierId = purchase.Supplier.Id;
            db.Suppliers.Attach(purchase.Supplier); // Prevent EF to create new 'purchase' entity -> http://pt.stackoverflow.com/a/5556/18246

            purchase.RegistrationDate = DateTime.Now;
            purchase.Items.ToList().ForEach(item => item.TotalCost = item.Quantity*item.UnitPrice);
            purchase.TotalCost = purchase.Items.Sum(item => item.TotalCost);

            foreach (var item in purchase.Items)
            {
                var product = await db.Products.FindAsync(item.ProductId);
                product.StockQuantity += item.Quantity;

                db.Entry(product).State = EntityState.Modified;
            }

            db.Purchase.Add(purchase);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = purchase.Id }, purchase);
        }

        // DELETE: api/Purchases/5
        [ResponseType(typeof(Purchase))]
        public async Task<IHttpActionResult> DeletePurchase(int id)
        {
            Purchase purchase = await db.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            db.Purchase.Remove(purchase);
            await db.SaveChangesAsync();

            return Ok(purchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseExists(int id)
        {
            return db.Purchase.Count(e => e.Id == id) > 0;
        }
    }
}