using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeaderForecastApi.Models;

namespace WeaderForecastApi.Controllers
{
    public class TemperaturesController : Controller
    {
        private TemperatureEntitys db = new TemperatureEntitys();

        // GET: Temperatures
        public async Task<ActionResult> Index()
        {
            return View(await db.Temperatures.ToListAsync());
        }

        // GET: Temperatures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temperatures temperatures = await db.Temperatures.FindAsync(id);
            if (temperatures == null)
            {
                return HttpNotFound();
            }
            return View(temperatures);
        }

        // GET: Temperatures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Temperatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CityName,Temperature")] Temperatures temperatures)
        {
            if (ModelState.IsValid)
            {
                db.Temperatures.Add(temperatures);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(temperatures);
        }

        // GET: Temperatures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temperatures temperatures = await db.Temperatures.FindAsync(id);
            if (temperatures == null)
            {
                return HttpNotFound();
            }
            return View(temperatures);
        }

        // POST: Temperatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CityName,Temperature")] Temperatures temperatures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temperatures).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(temperatures);
        }

        // GET: Temperatures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temperatures temperatures = await db.Temperatures.FindAsync(id);
            if (temperatures == null)
            {
                return HttpNotFound();
            }
            return View(temperatures);
        }

        // POST: Temperatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Temperatures temperatures = await db.Temperatures.FindAsync(id);
            db.Temperatures.Remove(temperatures);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
