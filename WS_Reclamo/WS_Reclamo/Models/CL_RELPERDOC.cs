namespace WS_Reclamo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CL_RelPerDoc
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal IdPersona { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string CodPais { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string TipoDoc { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string NroDocumento { get; set; }

        [StringLength(1)]
        public string TipoPer { get; set; }

        [StringLength(1)]
        public string Estado { get; set; }

        [StringLength(1)]
        public string Principal { get; set; }

        public DateTime? FecCaducidad { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TZ_LOCK { get; set; }

        public DateTime? FECHAEMISION { get; set; }
    }
}
