using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mosqueapp.Models;

namespace Mosqueapp.Controllers
{
    public class HadithsController : Controller
    {
        private mosqueappEntities db = new mosqueappEntities();

        // GET: Hadiths
        public ActionResult Index()
        {
            var hadiths = db.Hadiths.Include(h => h.MosqueLink);
            return View(hadiths.ToList());
        }

        // GET: Hadiths/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hadith hadith = db.Hadiths.Find(id);
            if (hadith == null)
            {
                return HttpNotFound();
            }
            return View(hadith);
        }

        // GET: Hadiths/Create
        public ActionResult Create()
        {
            ViewBag.Hadithid = new SelectList(db.MosqueLinks, "MosqueLinkid", "MosqueLinkid");
            return View();
        }

        // POST: Hadiths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hadithid,Hadith1")] Hadith hadith)
        {
            if (ModelState.IsValid)
            {
                db.Hadiths.Add(hadith);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Hadithid = new SelectList(db.MosqueLinks, "MosqueLinkid", "MosqueLinkid", hadith.Hadithid);
            return View(hadith);
        }

        // GET: Hadiths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hadith hadith = db.Hadiths.Find(id);
            if (hadith == null)
            {
                return HttpNotFound();
            }
            ViewBag.Hadithid = new SelectList(db.MosqueLinks, "MosqueLinkid", "MosqueLinkid", hadith.Hadithid);
            return View(hadith);
        }

        // POST: Hadiths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hadithid,Hadith1")] Hadith hadith)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hadith).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Hadithid = new SelectList(db.MosqueLinks, "MosqueLinkid", "MosqueLinkid", hadith.Hadithid);
            return View(hadith);
        }

        // GET: Hadiths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hadith hadith = db.Hadiths.Find(id);
            if (hadith == null)
            {
                return HttpNotFound();
            }
            return View(hadith);
        }

        // POST: Hadiths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hadith hadith = db.Hadiths.Find(id);
            db.Hadiths.Remove(hadith);
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
