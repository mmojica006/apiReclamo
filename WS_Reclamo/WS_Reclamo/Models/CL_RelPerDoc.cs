//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS_Reclamo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CL_RelPerDoc
    {
        public decimal IdPersona { get; set; }
        public string CodPais { get; set; }
        public string TipoDoc { get; set; }
        public string NroDocumento { get; set; }
        public string TipoPer { get; set; }
        public string Estado { get; set; }
        public string Principal { get; set; }
        public Nullable<System.DateTime> FecCaducidad { get; set; }
        public decimal TZ_LOCK { get; set; }
        public Nullable<System.DateTime> FECHAEMISION { get; set; }
    }
}
