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
using GoGreenV3.Models;
using GoGreenV3.Attributes;

namespace GoGreenV3.Controllers
{
    [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
    public class AgencyAPIController : ApiController
    {
        private AgencyDbContext db = new AgencyDbContext();

        // GET: api/AgencyAPI
        [AllowAnonymous]
        public IQueryable<AgencyModel> GetAgencies()
        {
            return db.Agencies;
        }

        // GET: api/AgencyAPI/5
        [AllowAnonymous]
        [ResponseType(typeof(AgencyModel))]
        public async Task<IHttpActionResult> GetAgency(int id)
        {
            AgencyModel agencyModel = await db.Agencies.FindAsync(id);
            if (agencyModel == null)
            {
                return NotFound();
            }

            return Ok(agencyModel);
        }

        // PUT: api/AgencyAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAgency(int id, AgencyModel agencyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agencyModel.Id)
            {
                return BadRequest();
            }

            db.Entry(agencyModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyModelExists(id))
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

        // POST: api/AgencyAPI
        [ResponseType(typeof(AgencyModel))]
        public async Task<IHttpActionResult> PostAgency(AgencyModel agencyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Agencies.Add(agencyModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = agencyModel.Id }, agencyModel);
        }

        // DELETE: api/AgencyAPI/5
        [ResponseType(typeof(AgencyModel))]
        public async Task<IHttpActionResult> DeleteAgency(int id)
        {
            AgencyModel agencyModel = await db.Agencies.FindAsync(id);
            if (agencyModel == null)
            {
                return NotFound();
            }

            db.Agencies.Remove(agencyModel);
            await db.SaveChangesAsync();

            return Ok(agencyModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgencyModelExists(int id)
        {
            return db.Agencies.Count(e => e.Id == id) > 0;
        }
    }
}