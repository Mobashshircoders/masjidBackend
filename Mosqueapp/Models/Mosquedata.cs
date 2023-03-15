using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mosqueapp.Models
{
    public class Mosquedata 
    {
        public int Masjidid { get; set; }
        public string Masjidname { get; set; }
        public string Designname { get; set; }
        public string Orientation { get; set; }

        public string Language { get; set; }
        public string City { get; set; }

        public string Fajrnamaaz { get; set; }
        public string Fajrazaan { get; set; }
        public string Shuruq { get; set; }
        
        public string Dhuhrazaan { get; set; }
        public string Dhuhrnamaaz { get; set; }
        
        public string Asrazaan { get; set; }
        public string Asrnamaaz { get; set; }
        
        public string Maghribazaan { get; set; }
        public string Maghribnamaaz { get; set; }
        
        public string Ishaazaan { get; set; }
        public string Ishanamaaz { get; set; }
        public string AlJumuanamaaz { get; set; }
        
    }
}