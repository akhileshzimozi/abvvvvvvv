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
using Blabyapp.API.DataModels;
using Blabyapp.API.ViewModels;

namespace Blabyapp.API.Controllers
{
    public class IndustriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Industries
        public IQueryable<Industry> GetIndustries()
        {
            return db.Industries;
        }

        // GET: api/Industries/5
        [ResponseType(typeof(Industry))]
        public async Task<IHttpActionResult> GetIndustry(string id)
        {
            Industry industry = await db.Industries.FindAsync(id);
            if (industry == null)
            {
                return NotFound();
            }

            return Ok(industry);
        }

        // PUT: api/Industries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIndustry(string id, Industry industry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != industry.IndustryID)
            {
                return BadRequest();
            }

            db.Entry(industry).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndustryExists(id))
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

        // POST: api/Industries
        [ResponseType(typeof(Industry))]
        public async Task<IHttpActionResult> PostIndustry(Industry industry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Industries.Add(industry);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IndustryExists(industry.IndustryID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = industry.IndustryID }, industry);
        }

        // DELETE: api/Industries/5
        [ResponseType(typeof(Industry))]
        public async Task<IHttpActionResult> DeleteIndustry(string id)
        {
            Industry industry = await db.Industries.FindAsync(id);
            if (industry == null)
            {
                return NotFound();
            }

            db.Industries.Remove(industry);
            await db.SaveChangesAsync();

            return Ok(industry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IndustryExists(string id)
        {
            return db.Industries.Count(e => e.IndustryID == id) > 0;
        }
    }
}