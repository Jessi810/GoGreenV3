using GoGreenV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoGreenV3.Controllers
{
    public class MonitorController : Controller
    {
        private MarkerDbContext db = new MarkerDbContext();

        // GET: Monitor
        public ActionResult Index()
        {
            int online = 0;
            int offline = 0;
            int workingCount = 0;
            int controllableCount = 0;
            foreach (var item in db.Markers)
            {
                if (item.Status.ToLower().Equals("online") || item.Status.ToLower().Contains("on")) online++;
                else offline++;
                if (item.IsWorking) workingCount++;
                if (item.IsControllable) controllableCount++;
            }

            ViewBag.OnlineCount = online;
            ViewBag.OfflineCount = offline;
            ViewBag.WorkingCount = workingCount;
            ViewBag.ControllableCount = controllableCount;

            return View(db.Markers.ToList());
        }
    }
}