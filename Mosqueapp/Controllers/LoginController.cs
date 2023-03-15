using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mosqueapp.common;
using Mosqueapp.Models;

namespace Mosqueapp.Controllers
{
    
    public class LoginController : Controller
    {
        mosqueappEntities db = new mosqueappEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string pass)
        {

            var cnt = db.Loginusers.Where(x => x.Username.ToLower().Trim() == username.ToLower().Trim() && pass == x.Password).ToList();
            if (cnt.Count > 0)
            {
                GetCitylists.lstWorldcity = db.Worldcities.ToList();
                FormsAuthentication.SetAuthCookie(cnt[0].Email, false);
                //Session["logintype"] = "Vendor";
                //Session["location"] = vd.Locationid.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return View("Index");
            }

        }

        public ActionResult Logout()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            //Session.Remove("logintype");
            //Session.Remove("location");
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}