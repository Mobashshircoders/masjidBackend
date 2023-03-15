using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Mosqueapp.common;
using Mosqueapp.Models;

namespace Mosqueapp.Controllers
{
    public class MosqueController : Controller
    {
        private mosqueappEntities db = new mosqueappEntities();
        // GET: Mosque
        public ActionResult Index()
        {
            List<mosquevm> lst = new List<mosquevm>();
            var input = db.Masjids.ToList();
            var languages = db.LanguageInputs.ToList();
            var prayertimes = db.Prayertimes.ToList();
            foreach(var data in input)
            {
                mosquevm obj = new mosquevm();
                obj.Masjidid = data.Masjidid;
                obj.Masjidname = data.Masjidname;
                obj.Language = languages.Where(x => x.LanguageInputId == data.Languageid.Value).First().Language;
                obj.Cityname = data.Cityname;
                obj.Designlayout = data.IsHorizontal.Value ? "Horizontal" : "Vertical";
                obj.Designname = data.Designname;
                obj.Prayertimeid = data.Prayertimeid.Value;
                if (data.IsHorizontal.Value)
                {
                    obj.Imagepath = "~/HorizontalDesign/" + obj.Designname + ".jpg";
                }
                else
                {
                    obj.Imagepath = "~/VerticalDesign/" + obj.Designname + ".jpg";
                }
                lst.Add(obj);
            }

            return View(lst);
        }

        public ActionResult Create()
        {
            ViewBag.LanguageInputId = new SelectList(db.LanguageInputs, "LanguageInputId", "Language");

            return View();
        }

        [HttpPost]
        public ActionResult Create(MasjidViewModel model, string Languageid, string design, string designname)
        {
            if (GetCitylists.lstWorldcity == null || GetCitylists.lstWorldcity.Count == 0)
            {
                GetCitylists.lstWorldcity = db.Worldcities.ToList();
            }
            List<Worldcity> cities = GetCitylists.lstWorldcity;
            var tempcityname = model.masjid.Cityname.ToLower().Trim();

            var cityobj = cities.Where(x => x.City.ToLower() == tempcityname).FirstOrDefault();
            if(cityobj != null)
            {
                //await GetNamaazTimeFromapi(model, cityobj);
            }
            Prayertime obj = model.prayertime;
            db.Prayertimes.Add(obj);
            db.SaveChanges();

            Masjid masjid = new Masjid();
            masjid.Masjidname = model.masjid.Masjidname;
            masjid.Cityname = model.masjid.Cityname;
            masjid.Designname = model.masjid.Designname;
            masjid.IsHorizontal = design == "horizontal" ? true : false;
            masjid.Languageid = Convert.ToInt32(Languageid);
            masjid.Prayertimeid = obj.Prayertimeid;
            db.Masjids.Add(masjid);
            db.SaveChanges();



            return View();
        }

        private static async Task GetNamaazTimeFromapi(MasjidViewModel model, Worldcity cityobj)
        {
            var lat = cityobj.Latitute.Value.ToString();
            var lng = cityobj.Longitude.Value.ToString();
            var method = "4";
            var month = DateTime.Now.Month.ToString();
            var year = DateTime.Now.Year.ToString();
            //var apicall = @" http://api.aladhan.com/v1/calendar?latitude=51.508515&longitude=-0.1254872&method=2&month=4&year=2017";
            //var apicall = @" http://api.aladhan.com/v1/hijriCalendarByCity?latitude="+lat+"&longitude="+lng+"&method="+method+"&month="+month+"&year="+year;
            var uristring = @"http://api.aladhan.com/v1/hijriCalendarByCity?";
            var values = @"city=" + model.masjid.Cityname + "&country=" + cityobj.Countryname + "&method=" + method + "&month=" + month + "&year=" + year;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" http://api.aladhan.com/v1/hijriCalendarByCity");
                //HTTP GET
                var responseTask = await client.GetAsync(uristring + values);



                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsStringAsync().Result;

                }
            }
        }


        [HttpPost]
        public JsonResult Getcities(string Prefix)
        {
            //Note : you can bind same list from database  
            if(GetCitylists.lstWorldcity == null || GetCitylists.lstWorldcity.Count == 0)
            {
                GetCitylists.lstWorldcity = db.Worldcities.ToList();
            }
            List<Worldcity> ObjList = GetCitylists.lstWorldcity;
            
            string lowprefix = Prefix.ToLower();
            //Searching records from list using LINQ query  
            var Name = (from N in ObjList
                        where N.City.ToLower().StartsWith(lowprefix)
                        select new { N.City });
            return Json(Name, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPrayertime(int id)
        {
            var input = db.Masjids.Where(x => x.Masjidid == id).First();
            //var languages = db.LanguageInputs.ToList();
            var prayertimes = db.Prayertimes.Where(x => x.Prayertimeid == input.Prayertimeid.Value).First();

            Mosquedata model = new Mosquedata();
            model.Masjidid = input.Masjidid;
            model.Fajrazaan = prayertimes.Fajrazaan;
            model.Fajrnamaaz = prayertimes.Fajrnamaaz;
            model.Dhuhrazaan = prayertimes.Dhuhrazaan;
            model.Dhuhrnamaaz = prayertimes.Dhuhrnamaaz;
            model.Asrazaan = prayertimes.Asrazaan;
            model.Asrnamaaz = prayertimes.Asrnamaaz;
            model.Maghribazaan = prayertimes.Maghribazaan;
            model.Maghribnamaaz = prayertimes.Maghribnamaaz;
            model.Ishaazaan = prayertimes.Ishaazaan;
            model.Ishanamaaz = prayertimes.Ishanamaaz;
            model.AlJumuanamaaz = prayertimes.AlJumuanamaaz;
            model.Shuruq = prayertimes.Shuruq;
            

            return View(model);
        }

        [HttpPost]
        public ActionResult EditPrayertime(Mosquedata obj)
        {
            var input = db.Masjids.Where(x => x.Masjidid == obj.Masjidid).First();
            Prayertime t = db.Prayertimes.Where(x => x.Prayertimeid == input.Prayertimeid).First();

            
            t.Fajrazaan = obj.Fajrazaan;
            t.Fajrnamaaz = obj.Fajrnamaaz;
            t.Dhuhrazaan = obj.Dhuhrazaan;
            t.Dhuhrnamaaz = obj.Dhuhrnamaaz;
            t.Asrazaan = obj.Asrazaan;
            t.Asrnamaaz = obj.Asrnamaaz;
            t.Maghribazaan = obj.Maghribazaan;
            t.Maghribnamaaz = obj.Maghribnamaaz;
            t.Ishaazaan = obj.Ishaazaan;
            t.Ishanamaaz = obj.Ishanamaaz;
            t.AlJumuanamaaz = obj.AlJumuanamaaz;
            t.Shuruq = obj.Shuruq;
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var input = db.Masjids.Where(x => x.Masjidid == id).First();
            ViewBag.LanguageInputId = new SelectList(db.LanguageInputs, "LanguageInputId", "Language");
            return View(input);
        }

        [HttpPost]
        public ActionResult Edit(Masjid obj, string Languageid, string design, string designname)
        {
            var input = db.Masjids.Where(x => x.Masjidid == obj.Masjidid).First();
            input.IsHorizontal = design == "horizontal" ? true : false;
            input.Languageid = Convert.ToInt32(Languageid);
            input.Designname = obj.Designname;
            db.Entry(input).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}