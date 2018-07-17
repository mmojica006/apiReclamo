using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WS_Reclamo.Models
{
    public class Reclamo
    {
       [Key]
        public int idReclamo { get; set; }
        public int MyProperty { get; set; }
        public string TipoEvento { get; set; }
        public string PaisDoc { get; set; }
        public string TipoDoc { get; set; }
        public string NroDoc { get; set; }
        public decimal Credito { get; set; }
        public int MedioComunic { get; set; }
        public int Categoria { get; set; }
        public int Subcategoria { get; set; }
        public string Comentario { get; set; }

    }
}