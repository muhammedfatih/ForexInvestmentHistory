using ForexHistory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForexHistory.Controllers
{
    public class MemberController : Controller
    {
        //
        // GET: /Member/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            bool isLogin = false;
            string id = (Session["id"] != null) ? Session["id"].ToString() : "-1";
            if (id != "-1") isLogin = true;
            else isLogin = false;
            if (isLogin)
            {
                member model = getMember(Convert.ToInt32(id));
                return View(model);
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Profile(FormCollection form)
        {
            bool isLogin = false;
            string id = (Session["id"] != null) ? Session["id"].ToString() : "-1";
            if (id != "-1") isLogin = true;
            else isLogin = false;
            if (isLogin)
            {
                ForexHistoryEntities db = new ForexHistoryEntities();
                member add = new member();
                add = db.member.Find(Convert.ToInt32(id));
                add.name = form["name"].Trim();
                add.surName = form["surName"].Trim();
                add.eMailAddress = form["eMailAddress"].Trim();
                add.setPassword(form["password"]);
                db.SaveChanges();
                return RedirectToAction("Index", "Forex");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Register()
        {
            bool isLogin = false;
            string id = (Session["id"] != null) ? Session["id"].ToString() : "-1";
            if (id != "-1") isLogin = true;
            else isLogin = false;
            if (isLogin) return RedirectToAction("Index", "Forex");
            else return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            bool isLogin = false;
            string id = (Session["id"] != null) ? Session["id"].ToString() : "-1";
            if (id != "-1") isLogin = true;
            else isLogin = false;
            if (isLogin) return RedirectToAction("Index", "Forex");
            else
            {
                ForexHistoryEntities db = new ForexHistoryEntities();
                member add = new member();
                add.name = form["name"].Trim();
                add.surName = form["surName"].Trim();
                add.eMailAddress = form["eMailAddress"].Trim();
                add.setPassword(form["password"]);
                db.member.Add(add);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
        }
        
        public ActionResult Login()
        {
            bool isLogin = false;
            string id = (Session["id"] != null) ? Session["id"].ToString() : "-1";
            if (id != "-1") isLogin = true;
            else isLogin = false;
            if (isLogin) return RedirectToAction("Index", "Forex");
            else return View();
        }

        public ActionResult Logout()
        {
            Session["id"] = null;
            Session["eMailAddress"] = null;
            Session["password"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            bool isLogin = false;
            string id2 = (Session["id"] != null) ? Session["id"].ToString() : "-1";
            if (id2 != "-1") isLogin = true;
            else isLogin = false;
            if (isLogin) return RedirectToAction("Index", "Forex");
            else
            {
                int id = isValidLogin(form["eMailAddress"], form["password"]);
                ForexHistoryEntities db = new ForexHistoryEntities();
                if (id != -1)
                {
                    member getOne = new member();
                    getOne = db.member.Find(id);
                    Session["id"] = getOne.id;
                    Session["eMailAddress"] = getOne.eMailAddress;
                    Session["password"] = getOne.password;
                    return RedirectToAction("Index", "Forex");
                }
                else
                {
                    Session["id"] = "-1";
                    Session["eMailAddress"] = "INVALID";
                    Session["password"] = "INVALID";
                    ViewBag.error = "Invalid login!";
                    return View();
                }
            }
        }

        public int isValidLogin(string eMailAddress, string password, bool crypt=true)
        {
            ForexHistoryEntities db = new ForexHistoryEntities();
            member model = new member();
            model.eMailAddress = eMailAddress;
            if (crypt) model.setPassword(password); else model.password = password;
            member resultMember = db.member.Where(x => x.eMailAddress == model.eMailAddress && x.password == model.password).SingleOrDefault();
            try
            {
                if (resultMember.id != 0) return resultMember.id;
                else return -1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public member getMember(int id)
        {
            ForexHistoryEntities db = new ForexHistoryEntities();
            member model = new member();
            model = db.member.Find(id);
            return model;
        }
    }
}
