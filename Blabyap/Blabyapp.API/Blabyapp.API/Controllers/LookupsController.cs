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
using Blabyap.Common.Model;

namespace Blabyapp.API.Controllers
{
    [RoutePrefix("api/Lookups")]
    public class LookupsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Lookups
        public IQueryable<Lookup> GetLookups()
        {
            return db.Lookups;
        }

       // [HttpGet]
        [ResponseType(typeof(List<CustomCaption>))]
       // [Route("GetLookUpExpertise")]
        public List<CustomCaption> GetLookUpExpertise()
        {
            var rslt = new List<CustomCaption>
            {
                new CustomCaption{Code="C1",Translation="C1"},
                new CustomCaption{Code="C2",Translation="C2"}
            };

            return rslt;
        }
        // GET: api/Lookups/5
        [ResponseType(typeof(Lookup))]
        public async Task<IHttpActionResult> GetLookup(string id)
        {
            Lookup lookup = await db.Lookups.FindAsync(id);
            if (lookup == null)
            {
                return NotFound();
            }

            return Ok(lookup);
        }

        // PUT: api/Lookups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLookup(string id, Lookup lookup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lookup.LookupId)
            {
                return BadRequest();
            }

            db.Entry(lookup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookupExists(id))
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

        // POST: api/Lookups
        [ResponseType(typeof(Lookup))]
        public async Task<IHttpActionResult> PostLookup(Lookup lookup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lookups.Add(lookup);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LookupExists(lookup.LookupId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lookup.LookupId }, lookup);
        }

        // DELETE: api/Lookups/5
        [ResponseType(typeof(Lookup))]
        public async Task<IHttpActionResult> DeleteLookup(string id)
        {
            Lookup lookup = await db.Lookups.FindAsync(id);
            if (lookup == null)
            {
                return NotFound();
            }

            db.Lookups.Remove(lookup);
            await db.SaveChangesAsync();

            return Ok(lookup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LookupExists(string id)
        {
            return db.Lookups.Count(e => e.LookupId == id) > 0;
        }
    }
}