﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoGreenV3.Models;
using GoGreenV3.Attributes;

namespace GoGreenV3.Controllers
{
    public class AgencyController : Controller
    {
        private AgencyDbContext db = new AgencyDbContext();
        
        // GET: Agency/List
        [AllowAnonymous]
        [Route(Name = "List")]
        public ActionResult AgencyList(string query)
        {
            var list = from a in db.Agencies select a;

            if (!String.IsNullOrEmpty(query))
            {
                list = list.Where(a => a.Name.Contains(query) || a.Type.Contains(query));
                return View(list.ToList());
            }

            list = from a in db.Agencies orderby a.Type select a;

            return View(list.ToList());
        }

        // GET: Agency
        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        public ActionResult Index(string query, string filter, string sortBy, string sortOrder)
        {
            var list = from a in db.Agencies select a;

            if (!String.IsNullOrEmpty(query))
            {
                list = list.Where(a => a.Name.Contains(query) || a.Type.Contains(query));
            }

            if (!String.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "Hospital":
                        list = list.Where(a => a.Type == "Hospital");
                        break;
                    case "Police Department":
                        list = list.Where(a => a.Type == "Police Department");
                        break;
                    case "Fire Station":
                        list = list.Where(a => a.Type == "Fire Station");
                        break;
                }
            }

            if (!String.IsNullOrEmpty(sortBy) && !String.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder.Equals("Ascending"))
                {
                    switch (sortBy)
                    {
                        case "Type":
                            list = list.OrderBy(a => a.Type);
                            break;
                        case "Name":
                            list = list.OrderBy(a => a.Name);
                            break;
                        case "Address":
                            list = list.OrderBy(a => a.Address);
                            break;
                        default:
                            list = list.OrderBy(a => a.Id);
                            break;
                    }
                }
                else
                {
                    switch (sortBy)
                    {
                        case "Type":
                            list = list.OrderByDescending(a => a.Type);
                            break;
                        case "Name":
                            list = list.OrderByDescending(a => a.Name);
                            break;
                        case "Address":
                            list = list.OrderByDescending(a => a.Address);
                            break;
                        default:
                            list = list.OrderByDescending(a => a.Id);
                            break;
                    }
                }
            }

            return View(list.ToList());
        }

        // GET: Agency/Details/5
        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgencyModel agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // GET: Agency/Create
        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        public ActionResult Create()
        {
            var types = GetAllTypes();
            var hospitals = GetAllHospitals();
            var polices = GetAllPoliceDepartments();
            var fires = GetAllFireStations();

            var model = new AgencyModel();

            model.Types = GetSelectListItems(types);
            model.Hospitals = GetSelectListItems(hospitals);
            model.PoliceDepartments = GetSelectListItems(polices);
            model.FireStations = GetSelectListItems(fires);

            return View(model);
        }

        // POST: Agency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        public ActionResult Create([Bind(Include = "Id,Type,Name,Address,Latitude,Longitude,Description,WebsiteUrl,Contact,Email")] AgencyModel model)
        {
            var types = GetAllTypes();
            var hospitals = GetAllHospitals();
            var polices = GetAllPoliceDepartments();
            var fires = GetAllFireStations();

            model.Types = GetSelectListItems(types);
            model.Hospitals = GetSelectListItems(hospitals);
            model.PoliceDepartments = GetSelectListItems(polices);
            model.FireStations = GetSelectListItems(fires);

            if (ModelState.IsValid)
            {
                db.Agencies.Add(model);
                db.SaveChanges();
                return RedirectToAction("SaveToFirebase", new { aid = model.Id });
            }

            return View(model);
        }

        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        public ActionResult SaveToFirebase(int aid)
        {
            var model = db.Agencies.Find(aid);
            return View(model);
        }

        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        public ActionResult DeleteToFirebase(int aid)
        {
            var model = db.Agencies.Find(aid);
            return View(model);
        }

        // GET: Agency/UpdateFirebase
        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        public ActionResult UpdateToFirebase()
        {
            var agencies = from a in db.Agencies select a;
            ViewBag.ItemCount = agencies.Count();

            return View("UpdateToFirebase", agencies.ToList());
        }

        private IEnumerable<string> GetAllTypes()
        {
            return new List<string>
            {
                "Hospital",
                "Police Department",
                "Fire Station"
            };
        }

        private IEnumerable<string> GetAllHospitals()
        {
            var agencies = from a in db.Agencies where a.Type == "Hospital" select a.Name;

            return agencies;
        }

        private IEnumerable<string> GetAllPoliceDepartments()
        {
            var agencies = from a in db.Agencies where a.Type == "Police Department" select a.Name;

            return agencies;
        }

        private IEnumerable<string> GetAllFireStations()
        {
            var agencies = from a in db.Agencies where a.Type == "Fire Stations" select a.Name;

            return agencies;
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }

        // GET: Agency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgencyModel agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        public ActionResult Edit([Bind(Include = "Id,Type,Name,Address,Latitude,Longitude,Description,WebsiteUrl,Contact,Email")] AgencyModel agency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SaveToFirebase", new { aid = agency.Id });
            }
            return View(agency);
        }

        // GET: Agency/Delete/5
        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgencyModel agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agency/Delete/5
        [AccessDeniedAuthorize(Roles = "Developer, Superuser, Admin, Moderator, Agent")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgencyModel agency = db.Agencies.Find(id);
            db.Agencies.Remove(agency);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("DeleteToFirebase", new { aid = agency.Id });
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
