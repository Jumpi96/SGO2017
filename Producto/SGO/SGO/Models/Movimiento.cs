//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGO.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Movimiento
    {
        public int ID { get; set; }
        public double Cantidad { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime ModFecha { get; set; }
        public Nullable<int> ModUsuarioID { get; set; }
        public string Observaciones { get; set; }
        public Nullable<int> ReceptorID { get; set; }
        public Nullable<int> ObraID { get; set; }
        public Nullable<int> SubItemID { get; set; }
    
        public virtual Obra Obra { get; set; }
        public virtual Receptor Receptor { get; set; }
        public virtual SubItem SubItem { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
