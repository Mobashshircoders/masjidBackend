//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mosqueapp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Masjid
    {
        public int Masjidid { get; set; }
        public string Masjidname { get; set; }
        public string Designname { get; set; }
        public string Country { get; set; }
        public string Cityname { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<int> Prayertimeid { get; set; }
        public Nullable<int> Languageid { get; set; }
        public string Logo { get; set; }
        public Nullable<bool> IsHorizontal { get; set; }
    
        public virtual MosqueLink MosqueLink { get; set; }
    }
}
