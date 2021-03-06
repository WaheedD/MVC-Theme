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
    
    public partial class aloAlojamientos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aloAlojamientos()
        {
            this.aloReservas = new HashSet<aloReservas>();
        }
    
        public int Id { get; set; }
        public string nroAlojamiento { get; set; }
        public short personas { get; set; }
        public bool inhabilitado { get; set; }
        public string caracteristicas { get; set; }
        public double precioTempAlta { get; set; }
        public double precioTempBaja { get; set; }
        public string agrup1 { get; set; }
        public string agrup2 { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string foto { get; set; }
        public Nullable<int> tipo_Id { get; set; }
        public string zona_Id { get; set; }
        public Nullable<int> bloqueoEspecial { get; set; }
    
        public virtual aloTipos aloTipos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aloReservas> aloReservas { get; set; }
    }
}
