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
    
    public partial class aloTipos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aloTipos()
        {
            this.aloAlojamientos = new HashSet<aloAlojamientos>();
            this.aloExcepciones = new HashSet<aloExcepciones>();
            this.aloSorteos = new HashSet<aloSorteos>();
        }
    
        public int Id { get; set; }
        public string nombre { get; set; }
        public string clase { get; set; }
        public string croquis { get; set; }
        public string foto { get; set; }
        public bool restricFDStempAlta { get; set; }
        public string idSede_idSede { get; set; }
        public System.DateTime habHasta { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aloAlojamientos> aloAlojamientos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aloExcepciones> aloExcepciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aloSorteos> aloSorteos { get; set; }
    }
}
