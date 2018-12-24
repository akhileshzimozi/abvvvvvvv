using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LocationAPI.Models;

namespace LocationAPI.Controllers
{
    public class GuidesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Guides
        public IQueryable<Guide> GetGuides()
        {
            return db.Guides;
        }

        [AllowAnonymous]
        // GET: api/Guides/5
        [ResponseType(typeof(Guide))]
        public IHttpActionResult GetGuide(int id)
        {
            Guide guide = db.Guides.Find(id);
            if (guide == null)
            {
                return NotFound();
            }

            return Ok(guide);
        }

        // PUT: api/Guides/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuide(int id, Guide guide)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guide.GuideID)
            {
                return BadRequest();
            }

            db.Entry(guide).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuideExists(id))
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

        [AllowAnonymous]
        // POST: api/Guides
        [ResponseType(typeof(Guide))]
        public IHttpActionResult PostGuide(Guide guide)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Guides.Add(guide);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = guide.GuideID }, guide);
        }

        // DELETE: api/Guides/5
        [ResponseType(typeof(Guide))]
        public IHttpActionResult DeleteGuide(int id)
        {
            Guide guide = db.Guides.Find(id);
            if (guide == null)
            {
                return NotFound();
            }

            db.Guides.Remove(guide);
            db.SaveChanges();

            return Ok(guide);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuideExists(int id)
        {
            return db.Guides.Count(e => e.GuideID == id) > 0;
        }
    }
}