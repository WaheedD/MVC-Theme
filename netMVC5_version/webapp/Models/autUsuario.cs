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
    
    public partial class autUsuario
    {
        public autUsuario()
        {
            this.aloReservas = new HashSet<aloReserva>();
            this.aloReservas1 = new HashSet<aloReserva>();
            this.genTicketsVentas = new HashSet<genTicketsVenta>();
            this.genTransaccionesVisanets = new HashSet<genTransaccionesVisanet>();
        }
    
        public string Id { get; set; }
        public string nroSocio { get; set; }
        public string email { get; set; }
        public byte[] Salt { get; set; }
        public byte[] SaltedAndHashedPassword { get; set; }
        public int idDepen { get; set; }
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
    
        public virtual ICollection<aloReserva> aloReservas { get; set; }
        public virtual ICollection<aloReserva> aloReservas1 { get; set; }
        public virtual ICollection<genTicketsVenta> genTicketsVentas { get; set; }
        public virtual ICollection<genTransaccionesVisanet> genTransaccionesVisanets { get; set; }
    }
}
