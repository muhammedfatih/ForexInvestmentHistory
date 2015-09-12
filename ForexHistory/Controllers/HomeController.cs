using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForexHistory.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            string id = (Session["id"] != null) ? Session["id"].ToString() : "-1";
            if (id != "-1") ViewBag.isLogin = true;
            else ViewBag.isLogin = false;

            return View();
        }

    }
}
