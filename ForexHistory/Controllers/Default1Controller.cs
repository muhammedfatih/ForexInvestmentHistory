using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForexHistory.Models;

namespace ForexHistory.Controllers
{
    public class Default1Controller : Controller
    {
        private ForexHistoryEntities db = new ForexHistoryEntities();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View(db.history.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            history history = db.history.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(history history)
        {
            if (ModelState.IsValid)
            {
                db.history.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(history);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            history history = db.history.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(history history)
        {
            if (ModelState.IsValid)
            {
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(history);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            history history = db.history.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            history history = db.history.Find(id);
            db.history.Remove(history);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}