//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HotelT
    {
        public int HotelTID { get; set; }
        public string NazivHotela { get; set; }
        public string Vlasnik { get; set; }
        public int RecepcijaID { get; set; }
    
        public virtual Recepcija Recepcija { get; set; }
    }
}
