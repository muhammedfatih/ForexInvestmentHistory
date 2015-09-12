using ForexHistory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForexHistory.Controllers
{
    public class ForexController : Controller
    {
        //
        // GET: /Forex/

        public ActionResult Index()
        {
            MemberController loginControl = new MemberController();
            string id = (Session["id"] != null) ? Session["id"].ToString() : "-1";
            string eMailAddress = (Session["eMailAddress"] != null) ? Session["eMailAddress"].ToString() : "INVALID";
            string password = (Session["password"] != null) ? Session["password"].ToString() : "INVALID";

            int nid=loginControl.isValidLogin(eMailAddress, password, false);
            if (id!="-1" && nid!=-1 && id==nid.ToString()){
                apiController a = new apiController();
                ViewBag.cur1 = "USD/TRY: " + a.get("getlatest", "usd", "try");
                ViewBag.cur2 = "EUR/TRY: " + a.get("getlatest", "eur", "try");
                ViewBag.cur3 = "GBP/TRY: " + a.get("getlatest", "gbp", "try");
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        public ActionResult List()
        {
            return View();
        }

    }
}
