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
    
    public partial class Zaduzenja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zaduzenja()
        {
            this.CheckIn = new HashSet<CheckIn>();
        }
    
        public int ZaduzenjaID { get; set; }
        public string VrstaZaduzenja { get; set; }
        public Nullable<double> Cijena { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckIn> CheckIn { get; set; }
    }
}