using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoGreenV3.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI;

namespace GoGreenV3.Controllers
{
    public class ReportController : Controller
    {
        private ReportDbContext db = new ReportDbContext();

        public void ExportToPDF(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Reports-GoGreen.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();

            Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            doc.AddAuthor("Go Green Team");
            doc.AddSubject("Go Green - Emergency Traffic Monitoring System");
            doc.AddTitle("Go Green");
            doc.SetMargins(8, 8, 8, 8);

            PdfWriter.GetInstance(doc, Response.OutputStream);

            doc.Open();

            PdfPTable table = new PdfPTable(6);
            table.AddCell("ID");
            table.AddCell("Date");
            table.AddCell("Time");
            table.AddCell("Location");
            table.AddCell("Latitude");
            table.AddCell("Longitude");

            DateTime date, time;
            foreach (var item in db.Alters)
            {
                table.AddCell((item.Id).ToString());
                if (DateTime.TryParse(item.Date, out date))
                {
                    table.AddCell(date.ToString("MM/dd/yyyy"));
                }
                else
                {
                    table.AddCell(item.Date);
                }

                if (DateTime.TryParse(item.Time, out time))
                {
                    table.AddCell(date.ToString("h:m:s tt"));
                }
                else
                {
                    table.AddCell(item.Time);
                }
                table.AddCell(item.Location);
                table.AddCell((item.Latitude).ToString());
                table.AddCell((item.Longitude).ToString());
            }

            doc.Add(new PdfPTable(table));

            doc.Close();
            sw.Close();
        }

        // GET: Report
        public async Task<ActionResult> Index()
        {
            return View(await db.Alters.ToListAsync());
        }

        // GET: Report/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlterReport alterReport = await db.Alters.FindAsync(id);
            if (alterReport == null)
            {
                return HttpNotFound();
            }
            return View(alterReport);
        }

        // GET: Report/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlterReport alterReport = await db.Alters.FindAsync(id);
            if (alterReport == null)
            {
                return HttpNotFound();
            }
            return View(alterReport);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AlterReport alterReport = await db.Alters.FindAsync(id);
            db.Alters.Remove(alterReport);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
