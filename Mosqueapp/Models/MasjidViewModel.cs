using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mosqueapp.Models
{
    public class MasjidViewModel
    {
        public Masjid masjid { get; set; }
        public Prayertime prayertime { get; set; }

        public string Language { get; set; }
        public string DesignLayout { get; set; }
    }
}