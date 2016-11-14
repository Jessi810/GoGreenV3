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

namespace GoGreenV3.Controllers
{
    public class ARApiController : ApiController
    {
        private ReportDbContext db = new ReportDbContext();

        // GET: api/ARApi
        public IQueryable<AlterReport> GetAlters()
        {
            return db.Alters;
        }

        // GET: api/ARApi/5
        [ResponseType(typeof(AlterReport))]
        public async Task<IHttpActionResult> GetAlter(int id)
        {
            AlterReport alterReport = await db.Alters.FindAsync(id);
            if (alterReport == null)
            {
                return NotFound();
            }

            return Ok(alterReport);
        }

        // PUT: api/ARApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlter(int id, AlterReport alterReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alterReport.Id)
            {
                return BadRequest();
            }

            db.Entry(alterReport).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlterReportExists(id))
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

        // POST: api/ARApi
        [ResponseType(typeof(AlterReport))]
        public async Task<IHttpActionResult> PostAlter(AlterReport alterReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var model = new AlterReport();
            //DateTime date, time;
            //DateTime.TryParse(alterReport.Date.ToString(), out date);
            //DateTime.TryParse(alterReport.Time.ToString(), out time);
            //model.Date = date;
            //model.Time = time;
            //model.Latitude = alterReport.Latitude;
            //model.Longitude = alterReport.Longitude;
            //model.Location = alterReport.Location;

            db.Alters.Add(alterReport);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alterReport.Id }, alterReport);
        }

        // DELETE: api/ARApi/5
        [ResponseType(typeof(AlterReport))]
        public async Task<IHttpActionResult> DeleteAlter(int id)
        {
            AlterReport alterReport = await db.Alters.FindAsync(id);
            if (alterReport == null)
            {
                return NotFound();
            }

            db.Alters.Remove(alterReport);
            await db.SaveChangesAsync();

            return Ok(alterReport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlterReportExists(int id)
        {
            return db.Alters.Count(e => e.Id == id) > 0;
        }
    }
}