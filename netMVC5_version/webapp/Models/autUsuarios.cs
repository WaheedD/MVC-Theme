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
    
    public partial class autUsuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public autUsuarios()
        {
            this.aloReservas = new HashSet<aloReservas>();
            this.aloReservas1 = new HashSet<aloReservas>();
            this.aloSorteoInscrip = new HashSet<aloSorteoInscrip>();
            this.appChoferes = new HashSet<appChoferes>();
            this.genTicketsVentas = new HashSet<genTicketsVentas>();
            this.genTransaccionesVisanet = new HashSet<genTransaccionesVisanet>();
        }
    
        public string Id { get; set; }
        public int idAsociado { get; set; }
        public string email { get; set; }
        public byte[] Salt { get; set; }
        public byte[] SaltedAndHashedPassword { get; set; }
        public short Consecutivo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string foto { get; set; }
        public System.DateTime fechaNac { get; set; }
        public System.DateTime ultimoLogin { get; set; }
        public string preguntaSecreta { get; set; }
        public string MD5respuestaSecreta { get; set; }
        public byte[] Version { get; set; }
        public System.DateTimeOffset CreatedAt { get; set; }
        public Nullable<System.DateTimeOffset> UpdatedAt { get; set; }
        public bool Deleted { get; set; }
        public string sexo { get; set; }
        public bool personalCRL { get; set; }
        public int personalCRLnivelAcceso { get; set; }
        public bool titular { get; set; }
        public string tokenInvitacion { get; set; }
        public string invitadoPor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aloReservas> aloReservas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aloReservas> aloReservas1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aloSorteoInscrip> aloSorteoInscrip { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appChoferes> appChoferes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<genTicketsVentas> genTicketsVentas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<genTransaccionesVisanet> genTransaccionesVisanet { get; set; }
    }
}
