using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Blabyapp.API.DataModels;
using Blabyapp.API.ViewModels;
namespace Blabyapp.API.Controllers
{
    [RoutePrefix("api/Match")]
    public class MatchController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("MatchCandidate")]
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> MatchCandidate(Match mymatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Match.Add(mymatch);

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mymatch.MatchID }, mymatch);
        }

        
        [ResponseType(typeof(Match))]
        [Route("UnMatchCandidate")]
        public async Task<IHttpActionResult> UnMatch(string candidatemail)
        {
            Match myMatch = db.Match.SingleOrDefault(u => u.CandidateEmail == candidatemail);

            if (myMatch == null)
            {
                return NotFound();
            }

            db.Match.Remove(myMatch);
            await db.SaveChangesAsync();

            return Ok(myMatch);
        }
    }
}
