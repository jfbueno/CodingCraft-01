using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using CodingCraft1.Context;
using CodingCraft1.Email;
using CodingCraft1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FluentDate;
using FluentDateTime;

namespace CodingCraft1.Controllers
{
    [Authorize]
    [RoutePrefix("api/sales")]
    public class SalesController : ApiController
    {
        private readonly MyContext _db = new MyContext();
        private readonly UserManager<IdentityUser> _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new MyContext()));

        // GET: api/Sales
        [Authorize(Roles = "admin")]
        public IQueryable<Sale> GetSales()
        {
            return _db.Sales.OrderByDescending(s => s.Date);
        }

        // GET: api/Sales/5
        [Authorize(Roles = "admin")]
        [Route("{id:int}")]
        [ResponseType(typeof(Sale))]
        public async Task<IHttpActionResult> GetSale(int id)
        {
            Sale sale = await _db.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        /// <summary>
        ///  Gets the sum of all sales costs in the current month
        /// </summary>
        /// <param name="baseDate">The base date to get the month</param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [Route("total-month-sales")]
        public async Task<IHttpActionResult> GetTotalMonthSales(DateTime baseDate)
        {
            var firstDatyOfMonth = baseDate.FirstDayOfMonth();
            var lasDayOfMonth = baseDate.LastDayOfMonth();

            var monthSales = _db.Sales
                                .Include(s => s.Items)
                                .Where(s => s.Date >= firstDatyOfMonth
                                         && s.Date <= lasDayOfMonth);

            var total = await monthSales.SumAsync(s => s.TotalCost);

            return Ok(total);
        }

        /// <summary>
        /// Gets the sum of all sales costs in the current month
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("profit-ballast-details")]
        public async Task<IHttpActionResult> GetProfitBallastOfMonth()
        {
            var firstDatyOfMonth = DateTime.Now.FirstDayOfMonth();
            var lasDayOfMonth = DateTime.Now.LastDayOfMonth();

            var query = _db.Sales.Where(x => x.Date >= firstDatyOfMonth).ToList();

            /*all sales made in this month*/
            var allSalesQuery = _db.Sales.Where(s => s.Date >= firstDatyOfMonth
                                                     && s.Date <= lasDayOfMonth);

            decimal salesTotal = 0;
            if (allSalesQuery.Any())
                salesTotal = await allSalesQuery.SumAsync(x => x.TotalCost);

            /*all purchases to pay in this month*/
            var allPurchasesQuery = _db.Purchase.Where(s => s.Payday >= firstDatyOfMonth
                                                        && s.Payday <= lasDayOfMonth);

            decimal purchasesTotal = 0;
            if (allPurchasesQuery.Any())
                purchasesTotal = await allPurchasesQuery.SumAsync(x => x.TotalCost);
            

            var ret = new
            {
                SalesTotal = salesTotal,
                PurchasesTotal = purchasesTotal
            };

            return Ok(ret);
        }
        
        // POST: api/Sales
        [ResponseType(typeof(Sale))]
        public async Task<IHttpActionResult> PostSale(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sale.Date = System.DateTime.Now;
            sale.TotalCost = sale.Items.Sum(x => x.TotalCost);
            
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return BadRequest("Consumer must be a registred user");
            }

            sale.ConsumerId = user.Id;

            _db.Sales.Add(sale);

            foreach (var item in sale.Items)
            {
                var baseProduct = await _db.Products.FindAsync(item.ProductId);
                baseProduct.StockQuantity -= item.Quantity;

                _db.Entry(baseProduct).State = EntityState.Modified;
            }

            await _db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = sale.Id }, sale);
            return Ok();
        }

        /// <summary>
        /// Sends an email to all consumers for sales in the current month
        /// </summary>
        /// <returns>(204 - No Content) if everything goes well</returns>
        [HttpPost]
        [Route("send-payment-reminders")]
        public async Task<IHttpActionResult> PaimentReminders()
        {
            //_db.Configuration.LazyLoadingEnabled = true;

            var firstDayOfMonth = DateTime.Now.FirstDayOfMonth();
            var lastDayOfMonth = DateTime.Now.LastDayOfMonth();

            var reminders = await _db.Sales
                                    .Include(sale => sale.Items)
                                    .Where(sale => sale.Date >= firstDayOfMonth && sale.Date <= lastDayOfMonth)
                                    .GroupBy(x => x.ConsumerId)
                                    .Select(x => new
                                            {
                                                UserId = x.Key,
                                                Sales = x.ToList(),
                                                AmountSales = x.Sum(s => s.TotalCost)
                                            })                            
                                    .ToListAsync();
            
            foreach (var reminder in reminders)
            {
                var user = await _userManager.FindByIdAsync(reminder.UserId);

                var salesCount = reminder.Sales.Count;

                var msg = $"Hi, {user.UserName}. Don't forget you have {salesCount} unpaid purchases with a total of {reminder.AmountSales:N2} =)\n" +
                            "Purchases details:\n";

                foreach (var sale in reminder.Sales)
                {
                    msg += $"Date: {sale.Date:dd/MM/yyyy}\nItems:";
                    foreach (var item in sale.Items)
                    {
                        msg += $"\n \t{item.BaseProduct.Description} - Quantity: {item.Quantity} - Unit price: {item.BaseProduct.SalePrice} - Total: {item.TotalCost}";
                    }

                    msg += "\n";
                }

                EmailHelper.SendEmail("Month purchases", msg, user.Email);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaleExists(int id)
        {
            return _db.Sales.Count(e => e.Id == id) > 0;
        }
    }
}