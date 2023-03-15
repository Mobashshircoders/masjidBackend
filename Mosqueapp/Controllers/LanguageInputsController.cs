using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mosqueapp.Models;

namespace Mosqueapp.Controllers
{
    public class LanguageInputsController : Controller
    {
        private mosqueappEntities db = new mosqueappEntities();

        // GET: LanguageInputs
        public ActionResult Index()
        {
            //ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();

            //foreach (TimeZoneInfo timeZone in timeZones)
            //{
            //    // store whatever you need to store to a SQL Server table
            //}
            var languageInputs = db.LanguageInputs;
            return View(languageInputs.ToList());
        }

        // GET: LanguageInputs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageInput languageInput = db.LanguageInputs.Find(id);
            if (languageInput == null)
            {
                return HttpNotFound();
            }
            return View(languageInput);
        }

        // GET: LanguageInputs/Create
        public ActionResult Create()
        {
            ViewBag.LanguageInputId = new SelectList(db.Masjids, "Masjidid", "Masjidname");
            return View();
        }

        // POST: LanguageInputs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LanguageInputId,Language")] LanguageInput languageInput)
        {
            if (ModelState.IsValid)
            {
                db.LanguageInputs.Add(languageInput);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageInputId = new SelectList(db.Masjids, "Masjidid", "Masjidname", languageInput.LanguageInputId);
            return View(languageInput);
        }

        // GET: LanguageInputs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageInput languageInput = db.LanguageInputs.Find(id);
            if (languageInput == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageInputId = new SelectList(db.Masjids, "Masjidid", "Masjidname", languageInput.LanguageInputId);
            return View(languageInput);
        }

        // POST: LanguageInputs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LanguageInputId,Language")] LanguageInput languageInput)
        {
            if (ModelState.IsValid)
            {
                db.Entry(languageInput).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageInputId = new SelectList(db.Masjids, "Masjidid", "Masjidname", languageInput.LanguageInputId);
            return View(languageInput);
        }

        // GET: LanguageInputs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageInput languageInput = db.LanguageInputs.Find(id);
            if (languageInput == null)
            {
                return HttpNotFound();
            }
            return View(languageInput);
        }

        // POST: LanguageInputs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LanguageInput languageInput = db.LanguageInputs.Find(id);
            db.LanguageInputs.Remove(languageInput);
            db.SaveChanges();
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
