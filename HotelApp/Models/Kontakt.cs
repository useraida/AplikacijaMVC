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
    using System.ComponentModel.DataAnnotations;

    public partial class Kontakt
    {
        public int KontaktID { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Poruka { get; set; }
        [Required]
        public string Telefon { get; set; }
    }
}
