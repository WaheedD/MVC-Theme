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
    
    public partial class genTicketsZonas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public genTicketsZonas()
        {
            this.genTicketsVentas = new HashSet<genTicketsVentas>();
        }
    
        public int id { get; set; }
        public string desc { get; set; }
        public double precio { get; set; }
        public int cupos { get; set; }
        public int vendidos { get; set; }
        public string croquis { get; set; }
        public int actividad_id { get; set; }
        public int porcSobreventa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<genTicketsVentas> genTicketsVentas { get; set; }
    }
}