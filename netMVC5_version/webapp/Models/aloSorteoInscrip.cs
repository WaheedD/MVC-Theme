//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartAdminMvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class aloSorteoInscrip
    {
        public int Id { get; set; }
        public string socio_id { get; set; }
        public int sorteo_id { get; set; }
        public System.DateTime fecha { get; set; }
        public bool ganador { get; set; }
        public string p1_id { get; set; }
        public string p2_id { get; set; }
        public string p3_id { get; set; }
        public Nullable<int> reserva_id { get; set; }
        public Nullable<int> carnet { get; set; }
    
        public virtual aloReservas aloReservas { get; set; }
        public virtual aloSorteos aloSorteos { get; set; }
        public virtual autUsuarios autUsuarios { get; set; }
    }
}
