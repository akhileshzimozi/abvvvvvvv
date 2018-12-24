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
    public class MeetNowsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MeetNows
        public IQueryable<MeetNow> GetMeetNows()
        {
            return db.MeetNows;
        }

        // GET: api/MeetNows/5
        [ResponseType(typeof(MeetNow))]
        public async Task<IHttpActionResult> GetMeetNow(string id)
        {
            MeetNow meetNow = await db.MeetNows.FindAsync(id);
            if (meetNow == null)
            {
                return NotFound();
            }

            return Ok(meetNow);
        }

        // PUT: api/MeetNows/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMeetNow(string id, MeetNow meetNow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meetNow.MeetNowID)
            {
                return BadRequest();
            }

            db.Entry(meetNow).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetNowExists(id))
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

        // POST: api/MeetNows
        [ResponseType(typeof(MeetNow))]
        public async Task<IHttpActionResult> PostMeetNow(MeetNow meetNow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MeetNows.Add(meetNow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MeetNowExists(meetNow.MeetNowID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = meetNow.MeetNowID }, meetNow);
        }

        // DELETE: api/MeetNows/5
        [ResponseType(typeof(MeetNow))]
        public async Task<IHttpActionResult> DeleteMeetNow(string id)
        {
            MeetNow meetNow = await db.MeetNows.FindAsync(id);
            if (meetNow == null)
            {
                return NotFound();
            }

            db.MeetNows.Remove(meetNow);
            await db.SaveChangesAsync();

            return Ok(meetNow);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetNowExists(string id)
        {
            return db.MeetNows.Count(e => e.MeetNowID == id) > 0;
        }
    }
}