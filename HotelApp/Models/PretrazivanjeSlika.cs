using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelApp.Models
{
    public class PretrazivanjeSlika<T>
    {
        // tip liste objekata kojima se moze pristupati jedino preko indeksa
        public List<T> Sadrzaj { get; set; }
        public Int32 TrenutnaStranica { get; set; }
        public Int32 VelicinaStranice { get; set; }
        public int Ukupno { get; set; }
    }
}