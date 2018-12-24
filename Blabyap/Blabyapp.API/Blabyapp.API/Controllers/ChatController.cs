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
    public class chatController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/chat
        public IQueryable<chat> Getchat()
        {
            return db.Chat;
        }

        // GET: api/chat/5
        [ResponseType(typeof(chat))]
        public async Task<IHttpActionResult> Getchat(string id)
        {
            chat chat = await db.Chat.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            return Ok(chat);
        }

        // PUT: api/chat/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putchat(int id, chat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chat.chatID)
            {
                return BadRequest();
            }

            db.Entry(chat).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!chatExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/chat
        [ResponseType(typeof(chat))]
        public async Task<IHttpActionResult> Postchat(chat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Chat.Add(chat);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //if (chatExists(chat.chatID))
                //{
                //    return Conflict();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return CreatedAtRoute("DefaultApi", new { id = chat.chatID }, chat);
        }

        // DELETE: api/chat/5
        [ResponseType(typeof(chat))]
        public async Task<IHttpActionResult> Deletechat(string id)
        {
            chat chat = await db.Chat.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            db.Chat.Remove(chat);
            await db.SaveChangesAsync();

            return Ok(chat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool chatExists(int id)
        {
            return db.Chat.Count(e => e.chatID == id) > 0;
        }
    }
}