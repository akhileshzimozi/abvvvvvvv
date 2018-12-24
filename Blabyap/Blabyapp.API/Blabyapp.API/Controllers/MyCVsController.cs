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
    public class MyCVsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MyCVs
        public IQueryable<MyCV> GetMyCVs()
        {
            return db.MyCVs;
        }

        // GET: api/MyCVs/5
        [Route("GetCV")]
        [ResponseType(typeof(CVData))]
        
        public async Task<IHttpActionResult> GetMyCV(string usermail)
        {
            MyCV userCV = await db.MyCVs.FirstAsync(u => u.UserMail == usermail);// (u => u.UserMail == User.Identity.Name);
            
            if (userCV == null)
            {
                return NotFound();
            }

            //MyCV myCV = await db.MyCVs.FindAsync(id);
            //if (myCV == null)
            //{
            //    return NotFound();
            //}
            CVData RetData = userCV;
            return Ok(RetData);
        }

        // PUT: api/MyCVs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMyCV(int id, MyCV myCV)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != myCV.MyCVID)
            {
                return BadRequest();
            }

            db.Entry(myCV).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MyCVExists(id))
                //{
                //    return NotFound();
                //}
                //else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MyCVs
        [Route("PostCV")]
        [ResponseType(typeof(MyCV))]

        public async Task<IHttpActionResult> PostMyCV(CVData myCVData)
        {
            MyCV myCV = myCVData;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(User!=null)
            {
                myCV.UserMail = User.Identity.Name;
                //myCV.UserMail = "a@a.com";
            }
              

            db.MyCVs.Add(myCV);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = myCV.MyCVID }, myCV);
        }

        // DELETE: api/MyCVs/5
        [ResponseType(typeof(MyCV))]
        public async Task<IHttpActionResult> DeleteMyCV(int id)
        {
            MyCV myCV = await db.MyCVs.FindAsync(id);
            if (myCV == null)
            {
                return NotFound();
            }

            db.MyCVs.Remove(myCV);
            await db.SaveChangesAsync();

            return Ok(myCV);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool MyCVExists(string id)
        //{
        //    return db.MyCVs.Count(e => e.MyCVID == id) > 0;
        //}
    }
}