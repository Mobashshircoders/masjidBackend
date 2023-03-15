using Mosqueapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mosqueapp.Controllers
{
    public class MasjidController : ApiController
    {
        private mosqueappEntities db = new mosqueappEntities();
        [HttpGet]
        public HttpResponseMessage Testapp()
        {
            
            try
            {
                string message = "Succeeded";
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(message));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }



        }

        [HttpGet]
        public HttpResponseMessage Getmosquename()
        {

            try
            {
                var lst = db.Masjids.Select(x => x.Masjidname).ToList();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(lst));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }



        }

        [HttpGet]
        public HttpResponseMessage Getmosquedata(string mosquename)
        {

            try
            {
                Mosquedata data = new Mosquedata();
                var obj = db.Masjids.Where(x => x.Masjidname.ToLower() == mosquename.ToLower().Trim()).FirstOrDefault();
                if(obj != null)
                {
                    Prayertime pt = db.Prayertimes.Where(x => x.Prayertimeid == obj.Prayertimeid.Value).FirstOrDefault();
                    data.Masjidid = obj.Masjidid;
                    data.Masjidname = obj.Masjidname;
                    data.Language = db.LanguageInputs.Where(x => x.LanguageInputId == obj.Languageid.Value).Select(x => x.Language).First();
                    data.City = obj.Cityname;
                    data.Orientation = obj.IsHorizontal.Value == true ? "Horizontal" : "Vertical";
                    data.Designname = obj.Designname;
                    data.Fajrazaan = pt.Fajrazaan;
                    data.Fajrnamaaz = pt.Fajrnamaaz;
                    data.Dhuhrazaan = pt.Dhuhrazaan;
                    data.Dhuhrnamaaz = pt.Dhuhrnamaaz;
                    data.Asrazaan = pt.Asrazaan;
                    data.Asrnamaaz = pt.Asrnamaaz;
                    data.Maghribazaan = pt.Maghribazaan;
                    data.Maghribnamaaz = pt.Maghribnamaaz;
                    data.Ishaazaan = pt.Ishaazaan;
                    data.Ishanamaaz = pt.Ishanamaaz;
                    data.AlJumuanamaaz = pt.AlJumuanamaaz;
                    data.Shuruq = pt.Shuruq;
                }

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(data));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }



        }
    }
}
