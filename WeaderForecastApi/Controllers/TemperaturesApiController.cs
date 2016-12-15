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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WeaderForecastApi.Hubs;
using WeaderForecastApi.Models;

namespace WeaderForecastApi.Controllers
{
    [EnableCors("*", "*", "GET,POST,PUT")]
    public class TemperaturesApiController : ApiControllerWithHub<TemperatureHub>
    {
        
        private TemperatureEntitys db = new TemperatureEntitys();

        // GET: api/TemperaturesApi
        [HttpGet]
        public IQueryable<Temperatures> GetTemperatures()
        {
            return db.Temperatures;
        }

        // GET: api/TemperaturesApi/5
        [ResponseType(typeof(Temperatures))]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemperatures(int id)
        {
            Temperatures temperatures = await db.Temperatures.FindAsync(id);
            if (temperatures == null)
            {
                return NotFound();
            }

            return Ok(temperatures);
        }

        // PUT: api/TemperaturesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTemperatures(int id, Temperatures temperatures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temperatures.Id)
            {
                return BadRequest();
            }

            db.Entry(temperatures).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                Hub.Clients.All.receiveTemperature(temperatures);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperaturesExists(id))
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

        // POST: api/TemperaturesApi
        [ResponseType(typeof(Temperatures))]
        public async Task<IHttpActionResult> PostTemperatures(Temperatures temperatures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Temperatures.Add(temperatures);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TemperaturesExists(temperatures.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = temperatures.Id }, temperatures);
        }

        // DELETE: api/TemperaturesApi/5
        [ResponseType(typeof(Temperatures))]
        public async Task<IHttpActionResult> DeleteTemperatures(int id)
        {
            Temperatures temperatures = await db.Temperatures.FindAsync(id);
            if (temperatures == null)
            {
                return NotFound();
            }

            db.Temperatures.Remove(temperatures);
            await db.SaveChangesAsync();

            return Ok(temperatures);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemperaturesExists(int id)
        {
            return db.Temperatures.Count(e => e.Id == id) > 0;
        }
        

    }
}