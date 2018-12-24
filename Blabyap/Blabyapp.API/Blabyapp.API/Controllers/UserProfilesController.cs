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
    public class UserProfilesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UserProfiles
        public IQueryable<UserProfile> GetUserProfiles()
        {
            return db.UserProfiles;
        }

        // GET: api/UserProfiles/5
        [ResponseType(typeof(UserProfile))]
        public async Task<IHttpActionResult> GetUserProfile(int id)
        {
            UserProfile userProfile = await db.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }

        // PUT: api/UserProfiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserProfile(int id, UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userProfile.UserID)
            {
                return BadRequest();
            }

            db.Entry(userProfile).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
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

        // POST: api/UserProfiles
        [ResponseType(typeof(UserProfile))]
        public async Task<IHttpActionResult> PostUserProfile(UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserProfiles.Add(userProfile);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userProfile.UserID }, userProfile);
        }

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(UserProfile))]
        public async Task<IHttpActionResult> DeleteUserProfile(int id)
        {
            UserProfile userProfile = await db.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            db.UserProfiles.Remove(userProfile);
            await db.SaveChangesAsync();

            return Ok(userProfile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserProfileExists(int id)
        {
            return db.UserProfiles.Count(e => e.UserID == id) > 0;
        }
    }
}