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
    
    public partial class DetalleSubItem
    {
        public int ID { get; set; }
        public double Cantidad { get; set; }
        public Nullable<int> ObraID { get; set; }
        public double PrecioUnitario { get; set; }
        public Nullable<int> SubItemDeItemID { get; set; }
    
        public virtual Obra Obra { get; set; }
        public virtual SubItemDeItem SubItemDeItem { get; set; }
    }
}
