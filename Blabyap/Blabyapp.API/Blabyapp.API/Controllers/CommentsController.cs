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
using Blabyap.Common.Model;
using Blabyapp.API.DataModels;
using Blabyapp.API.ViewModels;

namespace Blabyapp.API.Controllers
{
    public class CommentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Comments
        public IQueryable<Comments> GetComments()
        {

           // var commonmodel = new TestViewModel();

            return db.Comments;
        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comments))]
        public async Task<IHttpActionResult> GetComments(string id)
        {
            Comments comments = await db.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComments(string id, Comments comments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comments.CommentID)
            {
                return BadRequest();
            }

            db.Entry(comments).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsExists(id))
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

        // POST: api/Comments
        [ResponseType(typeof(Comments))]
        public async Task<IHttpActionResult> PostComments(Comments comments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comments);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommentsExists(comments.CommentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = comments.CommentID }, comments);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comments))]
        public async Task<IHttpActionResult> DeleteComments(string id)
        {
            Comments comments = await db.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comments);
            await db.SaveChangesAsync();

            return Ok(comments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentsExists(string id)
        {
            return db.Comments.Count(e => e.CommentID == id) > 0;
        }
    }
}