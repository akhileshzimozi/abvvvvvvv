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
    public class SenioritiesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Seniorities
        public IQueryable<Seniority> GetSeniorities()
        {
            return db.Seniorities;
        }

        // GET: api/Seniorities/5
        [ResponseType(typeof(Seniority))]
        public async Task<IHttpActionResult> GetSeniority(string id)
        {
            Seniority seniority = await db.Seniorities.FindAsync(id);
            if (seniority == null)
            {
                return NotFound();
            }

            return Ok(seniority);
        }

        // PUT: api/Seniorities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSeniority(string id, Seniority seniority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seniority.SeniorityID)
            {
                return BadRequest();
            }

            db.Entry(seniority).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeniorityExists(id))
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

        // POST: api/Seniorities
        [ResponseType(typeof(Seniority))]
        public async Task<IHttpActionResult> PostSeniority(Seniority seniority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Seniorities.Add(seniority);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SeniorityExists(seniority.SeniorityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = seniority.SeniorityID }, seniority);
        }

        // DELETE: api/Seniorities/5
        [ResponseType(typeof(Seniority))]
        public async Task<IHttpActionResult> DeleteSeniority(string id)
        {
            Seniority seniority = await db.Seniorities.FindAsync(id);
            if (seniority == null)
            {
                return NotFound();
            }

            db.Seniorities.Remove(seniority);
            await db.SaveChangesAsync();

            return Ok(seniority);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeniorityExists(string id)
        {
            return db.Seniorities.Count(e => e.SeniorityID == id) > 0;
        }
    }
}