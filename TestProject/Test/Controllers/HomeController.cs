using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Test.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context db = new Context();
        public ActionResult Index(int? client)
        {
            IQueryable<Order> orders = db.Orders.Include(c => c.Client);
            if (client != null && client != 0)
            {
                orders = orders.Where(c => c.ClientId == client);
            }

            ViewBag.Orders = orders;
            List<Client> clients = db.Clients.ToList();
            ViewBag.Clients = clients;
            return View();
        }
        
        public async Task<ActionResult> ViewClients()
        {
            IEnumerable<Client> clients = await db.Clients.ToListAsync();
            ViewBag.Clients = clients;
            return View();
        }
        
        public async Task<ActionResult> ViewOrders()
        {
            IEnumerable<Order> orders = await db.Orders.ToListAsync();
            ViewBag.Orders = orders;
            return View();
        }

        [HttpGet]
        public ActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateClient(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("ViewClients");
            }
            return View(client);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteClient(int id)
        {
            Client c = await db.Clients.FindAsync(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        [HttpPost, ActionName("DeleteClient")]
        public async Task<ActionResult> DeleteClientConfirmed(int id)
        {
            Client c = await db.Clients.FindAsync(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            db.Clients.Remove(c);
            await db.SaveChangesAsync();
            return RedirectToAction("ViewClients");
        }

        [HttpGet]
        public ActionResult CreateOrder()
        {
            SelectList clients = new SelectList(db.Clients, "Id", "Name");
            ViewBag.Clients = clients;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            if (ModelState.IsValid) 
            { 
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("ViewOrders");
            }
            SelectList clients = new SelectList(db.Clients, "Id", "Name");
            ViewBag.Clients = clients;
            return View(order);
        }

        [HttpGet]
        public async Task<ActionResult> EditOrder(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Order order = await db.Orders.FindAsync(id);
            if (order != null)
            {
                SelectList clients = new SelectList(db.Clients, "Id", "Name", order.ClientId);
                ViewBag.Clients = clients;
                return View(order);
            }
            return RedirectToAction("ViewOrders");
        }

        [HttpPost]
        public async Task<ActionResult> EditOrder(Order order)
        {
            db.Entry(order).State  = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("ViewOrders");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            Order o = await db.Orders.FindAsync(id);
            if (o == null)
            {
                return HttpNotFound();
            }
            return View(o);
        }

        [HttpPost, ActionName("DeleteOrder")]
        public async Task<ActionResult> DeleteOrderConfirmed(int id)
        {
            Order o = await db.Orders.FindAsync(id);
            db.Orders.Remove(o);
            await db.SaveChangesAsync();
            return RedirectToAction("ViewOrders");
        }
        
        [HttpGet]
        public ActionResult OrdersClient(int? id)
        {
            var orders = db.Orders.Where(a => a.ClientId == id).ToList();
            var client = db.Clients.Where(a => a.Id == id);
            ViewBag.ClientName = client.First().Name;

            return PartialView(orders);
        }
    }
}
