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
    public class HistoryItemController : Controller
    {
        private ForexHistoryEntities db = new ForexHistoryEntities();

        //
        // GET: /HistoryItem/

        public ActionResult Index()
        {
            int _userid = (Session["id"] != null) ? Convert.ToInt32(Session["id"].ToString()) : -1;
            apiController a = new apiController();
            ViewBag.cur1 = "USD/TRY: " + a.get("getlatest", "usd", "try");
            ViewBag.cur2 = "EUR/TRY: " + a.get("getlatest", "eur", "try");
            ViewBag.cur3 = "GBP/TRY: " + a.get("getlatest", "gbp", "try");
            ForexHistoryEntities db = new ForexHistoryEntities();
            List<history> historyList = db.history.Where(x => x.userId == _userid).ToList();
            return View(historyList);
        }

        //
        // GET: /HistoryItem/Details/5

        public ActionResult Details(int id = 0)
        {
            int _userid = (Session["id"] != null) ? Convert.ToInt32(Session["id"].ToString()) : -1;
            apiController a = new apiController();
            ViewBag.cur1 = "USD/TRY: " + a.get("getlatest", "usd", "try");
            ViewBag.cur2 = "EUR/TRY: " + a.get("getlatest", "eur", "try");
            ViewBag.cur3 = "GBP/TRY: " + a.get("getlatest", "gbp", "try");

            history history = db.history.Find(id);
            if (history == null || history.userId!=_userid)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        //
        // GET: /HistoryItem/Create

        public ActionResult Create()
        {
            apiController a = new apiController();
            ViewBag.cur1 = "USD/TRY: " + a.get("getlatest", "usd", "try");
            ViewBag.cur2 = "EUR/TRY: " + a.get("getlatest", "eur", "try");
            ViewBag.cur3 = "GBP/TRY: " + a.get("getlatest", "gbp", "try");

            return View();
        }

        //
        // POST: /HistoryItem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(history history)
        {
            apiController a = new apiController();
            ViewBag.cur1 = "USD/TRY: " + a.get("getlatest", "usd", "try");
            ViewBag.cur2 = "EUR/TRY: " + a.get("getlatest", "eur", "try");
            ViewBag.cur3 = "GBP/TRY: " + a.get("getlatest", "gbp", "try");

            string id = (Session["id"] != null) ? Session["id"].ToString() : "-1";
            int intId = Convert.ToInt32(id);
            history.userId = intId;
            history.result=a.get(history.date1, history.date2, history.cur1, history.cur2, history.amount);
            if (ModelState.IsValid)
            {
                db.history.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(history);
        }

        //
        // GET: /HistoryItem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            int _userid = (Session["id"] != null) ? Convert.ToInt32(Session["id"].ToString()) : -1;
            apiController a = new apiController();
            ViewBag.cur1 = "USD/TRY: " + a.get("getlatest", "usd", "try");
            ViewBag.cur2 = "EUR/TRY: " + a.get("getlatest", "eur", "try");
            ViewBag.cur3 = "GBP/TRY: " + a.get("getlatest", "gbp", "try");

            history history = db.history.Find(id);
            if (history == null || history.userId!=_userid)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        //
        // POST: /HistoryItem/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(history history)
        {
            apiController a = new apiController();
            ViewBag.cur1 = "USD/TRY: " + a.get("getlatest", "usd", "try");
            ViewBag.cur2 = "EUR/TRY: " + a.get("getlatest", "eur", "try");
            ViewBag.cur3 = "GBP/TRY: " + a.get("getlatest", "gbp", "try");

            if (ModelState.IsValid)
            {
                string sid = (Session["id"] != null) ? Session["id"].ToString() : "-1";
                int intId = Convert.ToInt32(sid);
                history.userId = intId;

                db.Entry(history).State = EntityState.Modified;
                history.result = a.get(history.date1, history.date2, history.cur1, history.cur2, history.amount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(history);
        }

        //
        // GET: /HistoryItem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            int _userid = (Session["id"] != null) ? Convert.ToInt32(Session["id"].ToString()) : -1;
            apiController a = new apiController();
            ViewBag.cur1 = "USD/TRY: " + a.get("getlatest", "usd", "try");
            ViewBag.cur2 = "EUR/TRY: " + a.get("getlatest", "eur", "try");
            ViewBag.cur3 = "GBP/TRY: " + a.get("getlatest", "gbp", "try");

            history history = db.history.Find(id);
            if (history == null || history.userId!=_userid)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        //
        // POST: /HistoryItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            apiController a = new apiController();
            ViewBag.cur1 = "USD/TRY: " + a.get("getlatest", "usd", "try");
            ViewBag.cur2 = "EUR/TRY: " + a.get("getlatest", "eur", "try");
            ViewBag.cur3 = "GBP/TRY: " + a.get("getlatest", "gbp", "try");

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