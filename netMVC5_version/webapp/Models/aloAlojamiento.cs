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
    
    public partial class aloAlojamiento
    {
        public aloAlojamiento()
        {
            this.aloReservas = new HashSet<aloReserva>();
        }
    
        public string Id { get; set; }
        public short personas { get; set; }
        public bool inhabilitado { get; set; }
        public string caracteristicas { get; set; }
        public double precioTempAlta { get; set; }
        public double precioTempBaja { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public Nullable<int> tipo_Id { get; set; }
        public string foto { get; set; }
        public string agrup1 { get; set; }
        public string agrup2 { get; set; }
        public string zona_Id { get; set; }
    
        public virtual aloTipos aloTipos { get; set; }
        public virtual aloZona aloZona { get; set; }
        public virtual ICollection<aloReserva> aloReservas { get; set; }
    }
}